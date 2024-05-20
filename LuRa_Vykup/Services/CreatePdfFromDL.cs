using LuRa_Vykup.Controllers;
using LuRa_Vykup.Data;
using LuRa_Vykup.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Diagnostics;



namespace LuRa_Vykup.Services
{
    public class CreatePdfFromDL
    {
        private readonly ApplicationDbContext _context;

        public CreatePdfFromDL(ApplicationDbContext context)
        {
            _context = context;
        }
        // properta pro list vykupu
        private List<Vykup> VykupList { get; set; }
        private string CisloDL { get; set; }
        private string DatumVystaveni { get; set; }
        private decimal CelkovaVaha { get; set; }
        private string Vykupna { get; set; }
        private string Ulice { get; set; }
        private string CisloPopisne { get; set; }
        private string Mesto { get; set; }
        private string PSC { get; set; }

        private string Dodavatel { get; set; }

        private int IdVykupna { get; set; } = 0;
        private int IdDl { get; set; } = 0;

        private Mena Mena { get; set; }

        public void NewPdf(int id_dl)
        {

            var dlController = new DLController(_context);
            VykupList = dlController.GetVykupForDL(id_dl);
            
            // ziskani cisla DL
            CisloDL = VykupList.FirstOrDefault().DodaciListy.CisloDL;
            DatumVystaveni = VykupList.FirstOrDefault().DodaciListy.DatumVystaveni.Value.ToShortDateString();
            CelkovaVaha = VykupList.Sum(x => x.Vaha);
            Vykupna = VykupList.FirstOrDefault().Vykupna.Nazev;
            Ulice = VykupList.FirstOrDefault().Vykupna.Ulice;
            CisloPopisne = VykupList.FirstOrDefault().Vykupna.CisloPopisne;
            Mesto = VykupList.FirstOrDefault().Vykupna.Mesto;
            PSC = VykupList.FirstOrDefault().Vykupna.Psc;
            IdVykupna = VykupList.FirstOrDefault().Vykupna.Id;
            IdDl = VykupList.FirstOrDefault().DodaciListy.Id;
            Dodavatel = VykupList.FirstOrDefault().Dodavatele.Dodavatel;
            Mena = Mena.CZK;
            CreatePDF();
        }


        /// <summary>
        /// Hlavicka
        /// </summary>
        /// <param name="container"></param>
        void DLHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column
                        .Item().Text($"{Dodavatel}")
                        .FontSize(18).SemiBold().FontColor(Colors.Blue.Medium);

                    column.Item().Text(text =>
                    {
                        text.Span("Datum: ").SemiBold();
                        text.Span($"{DatumVystaveni:d}");
                    });
                    column.Item().Text(text =>
                    {
                        text.Span("Slouží pouze jako podklad pro fakturaci.").SemiBold();
                    });

                });

                row.RelativeItem().Column(column =>
                {

                    column
                        .Item().Text($"Dodací list #{CisloDL}")
                        .FontSize(18).SemiBold().FontColor(Colors.Blue.Medium);
                  

                });

            });
        }


        public Task CreatePDF()
        {

            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            DocumentMetadata metadata = new DocumentMetadata
            {
                Title = "Dodací list",
                Author = "LuRa Výkup",
                Subject = "Dodací list",
                Keywords = "Dodací list",
                Creator = "LuRa Výkup",
                Producer = "LuRa Výkup"
            };


            
            Document.Create(container =>
            {
                container
                .Page(page =>
                {
                    page.MarginVertical(30);
                    page.MarginHorizontal(50);

                    page.Header().Element(DLHeader); // odkaz na hlavicku
                    page.Content().Element(DLContent); // odkaz na telo

                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.CurrentPageNumber();
                        text.Span(" / ");
                        text.TotalPages();
                    });
                });
            }).GeneratePdfAndShowVlastni(CisloDL, IdDl);


            return Task.CompletedTask;
        }


        /// <summary>
        /// Telo dokumentu
        /// </summary>
        /// <param name="container"></param>
        void DLContent(IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(20);


                column.Item().Element(DLTable);

                var totalPrice = VykupList.TrueForAll(x => x.CenaKg == 0) ? 0 : VykupList.Sum(x => x.Vaha * x.CenaKg);
                column.Item().PaddingRight(5).AlignRight().Text($"Cena celkem: {CenaKgToCurrency(totalPrice)}").SemiBold();
                var totalKg = VykupList.Sum(x => x.Vaha);
                column.Item().PaddingRight(5).AlignRight().Text($"Váha celkem: {totalKg:N2} kg").SemiBold();


            });
        }

        /// <summary>
        /// Tabulka
        /// </summary>
        /// <param name="container"></param>
        void DLTable(IContainer container)
        {
            var headerStyle = TextStyle.Default.SemiBold();

            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(25);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().Text("#");
                    header.Cell().Text("Plomba").Style(headerStyle);
                    header.Cell().AlignRight().Text("Váha").Style(headerStyle);
                    header.Cell().AlignRight().Text("Druh").Style(headerStyle);
                    header.Cell().AlignRight().Text("Kategorie").Style(headerStyle);
                    header.Cell().AlignRight().Text("Cena kg").Style(headerStyle);
                    header.Cell().AlignRight().Text("Celkem").Style(headerStyle);

                    header.Cell().ColumnSpan(7).PaddingTop(5).BorderBottom(1).BorderColor(Colors.Black);
                });

                int index = 0;
                foreach (var item in VykupList)
                {
                    index++;

                    table.Cell().Element(CellStyle).Text($"{index}");
                    table.Cell().Element(CellStyle).Text(item.Plomba);
                    table.Cell().Element(CellStyle).AlignRight().Text($"{item.Vaha:N2} Kg");
                    table.Cell().Element(CellStyle).AlignRight().Text(item.Druh);
                    table.Cell().Element(CellStyle).AlignRight().Text($"{item.Kategorie}");
                    table.Cell().Element(CellStyle).AlignRight().Text($"{CenaKgToCurrency(item.CenaKg)}");
                    table.Cell().Element(CellStyle).AlignRight().Text($"{CenaKgToCurrency(item.Vaha * item.CenaKg)}");

                    static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                }
            });
        }


        private string CenaKgToCurrency(decimal? cislo)
        {
            if (Mena == Mena.EUR)
                return $"{cislo:N2} EUR";
            return $"{cislo:N2} CZK";
        }













        //[Obsolete]
        //public Task CreatePDF()
        //{
        //    QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
        //    DocumentMetadata metadata = new DocumentMetadata
        //    {
        //        Title = "Dodací list",
        //        Author = "LuRa Výkup",
        //        Subject = "Dodací list",
        //        Keywords = "Dodací list",
        //        Creator = "LuRa Výkup",
        //        Producer = "LuRa Výkup"
        //    };
        //   Document.Create(container =>
        //    {
        //        container.Page(page =>
        //        {
        //            page.MarginHorizontal(40);
        //            page.MarginVertical(60);
        //            page.Size(PageSizes.A4);
        //            //Nastaveni hlavicky
        //            page.Header()
        //            .Text(text =>
        //                {
        //                    text.Span("Dodací list číslo: " + CisloDL).Underline().FontSize(20).Bold();

        //                }); 
        //            page.Content().PaddingVertical(25).Grid(grid =>
        //            {

        //                grid.VerticalSpacing(5);
        //                grid.HorizontalSpacing(0);
        //                grid.AlignCenter();
        //                grid.Columns(12);

        //                grid.Item(12).Height(30).AlignLeft().AlignMiddle().PaddingLeft(5).Text(Dodavatel).FontSize(15).Bold();


        //                grid.Item(2).Background(Colors.Grey.Lighten1).Height(20).AlignMiddle().PaddingLeft(5).Text("Plomba výkupu").FontSize(10).Bold();
        //                grid.Item(2).Background(Colors.Grey.Lighten1).Height(20).AlignRight().AlignMiddle().Text("Váha ").FontSize(10).Bold();
        //                grid.Item(3).Background(Colors.Grey.Lighten1).AlignCenter().Height(20).AlignMiddle().Text("Druh").FontSize(10).Bold();
        //                grid.Item(2).Background(Colors.Grey.Lighten1).AlignCenter().Height(20).AlignMiddle().Text("Kategorie").FontSize(10).Bold();
        //                grid.Item(3).Background(Colors.Grey.Lighten1).AlignCenter().Height(20).AlignMiddle().Text("Prohližitel ").FontSize(10).Bold();
        //                foreach (var item in VykupList)
        //                {

        //                    grid.Item(2).BorderBottom(1, Unit.Point).Height(15).PaddingLeft(5).Text(item.Plomba).FontSize(10);
        //                    grid.Item(2).BorderBottom(1, Unit.Point).Height(15).AlignRight().Text(item.Vaha+ " kg ").FontSize(10);
        //                    grid.Item(3).BorderBottom(1, Unit.Point).Height(15).AlignCenter().Text(item.Druh).FontSize(10);
        //                    grid.Item(2).BorderBottom(1, Unit.Point).AlignCenter().Height(15).Text(" "+item.Kategorie).FontSize(10);
        //                    grid.Item(3).BorderBottom(1, Unit.Point).AlignCenter().Height(15).Text(item.CisloM).FontSize(10);

        //                }

        //                grid.Item(12).PaddingTop(5).Background(Colors.Grey.Lighten1).PaddingRight(5).AlignRight().AlignMiddle().Height(20).Text("Celková váha: " + CelkovaVaha+ " kg ").FontSize(12).DecorationDouble();
        //            });
        //        });
        //    }).GeneratePdfAndShowVlastni(CisloDL, IdDl);

        //    return Task.CompletedTask;
        //}


    }



}



