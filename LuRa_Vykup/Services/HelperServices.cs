using BlazorBootstrap;
using LuRa_Vykup.Controllers;
using LuRa_Vykup.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.JSInterop;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Diagnostics;

namespace LuRa_Vykup.Services
{

    public static class HelperServices
    {
        #region Dodacli Listy
        public static string NewDLNumber { get; set; }

        #endregion

        #region Pokladni Doklady
        public static string NewPDNumber { get; set; }
        public static string NewPDDodavatel { get; set; }
        public static int NewPDCenikId { get; set; }

        #endregion

        #region Traces Doklady
        public static string NewTracesNumber { get; set; }
        

        #endregion

        #region Vykupni Doklady
        public static int IdVykupna { get; set; } = 0;
        #endregion

        #region Styly pro fixaci gridu
        private static string FixString => "<style>th div.d-flex {height: 30px !important;} button span {font-size: 0.8rem !important;}</style>";
        /// <summary>
        /// Zafixuje velikost vyhledavače a tlačítek v gridu
        /// </summary>
        /// <returns></returns>
        public static MarkupString GetGridFix() => new MarkupString(FixString);


        public static async Task<IEnumerable<FilterOperatorInfo>> PrekladFilter()
        {
            var filtersTranslation = new List<FilterOperatorInfo>();

            // number/date/boolean
            filtersTranslation.Add(new("=", "Je rovno", FilterOperator.Equals));
            filtersTranslation.Add(new("!=", "Není rovno", FilterOperator.NotEquals));
            // number/date
            filtersTranslation.Add(new("<", "Menší než", FilterOperator.LessThan));
            filtersTranslation.Add(new("<=", "Menší nebo rovno", FilterOperator.LessThanOrEquals));
            filtersTranslation.Add(new(">", "Větší", FilterOperator.GreaterThan));
            filtersTranslation.Add(new(">=", "Větší nebo rovno", FilterOperator.GreaterThanOrEquals));
            // string
            filtersTranslation.Add(new("*a*", "Obsahuje", FilterOperator.Contains));
            filtersTranslation.Add(new("a**", "Končít", FilterOperator.StartsWith));
            filtersTranslation.Add(new("**a", "Začíná", FilterOperator.EndsWith));
            filtersTranslation.Add(new("=", "Stejný", FilterOperator.Equals));
            // common
            filtersTranslation.Add(new("x", "Zrušit filtry", FilterOperator.Clear));

            return await Task.FromResult(filtersTranslation);
        }
        #endregion

        #region Generovani čísla dokladu
        /// <summary>
        /// Generator nového čísla dodacího listu
        /// Prázdný string nebo null - vrací první číslo pro daný rok
        /// Pokud se liší rok, vrací první číslo pro daný rok
        /// </summary>
        /// <param name="posledniCislo">Poslední číslo z DB</param>
        /// <param name="prefix">Prefix dokladu kde enumu, použij "Prefix"</param>
        /// <returns></returns>
        public static string GeneratorDokladu(string posledniCislo, Prefix prefix)
        {
            // pokud je posledni prazdny, vraci prvni cislo pro dany rok + prechod do nového roku
            if (string.IsNullOrEmpty(posledniCislo) || posledniCislo.Substring(2, 4) != DateTime.Today.Year.ToString())
            {
                return prefix + DateTime.Today.Year.ToString() + "00001";
            }

            //DL 2024 00001 - posledni
            string datum = DateTime.Today.Year.ToString();
            string zacatek = prefix.ToString();

            string rokDL = posledniCislo.Substring(2, 4); //2024 
            string cisloDL = posledniCislo.Substring(6, 5); //00001

            if (datum != rokDL)
            {
                posledniCislo = "00000";
            }
            
            string novecislo = (int.Parse(cisloDL) + 1).ToString();
            int pocetmist = 5 - novecislo.Length;
            string x = string.Empty;
            if (pocetmist == 4) { x = "0000"; };
            if (pocetmist == 3) { x = "000"; };
            if (pocetmist == 2) { x = "00"; };
            if (pocetmist == 1) { x = "0"; };

            return zacatek + datum + x + novecislo;
        }

        public enum Prefix
        {
            DL,
            PD,
            TR
        }
        #endregion

        #region Generovani PDF
        public static void GeneratePdfAndShowVlastni(this IDocument document, string cisloDL, int dlId)
        {
            try
            {
                var rootFileName = $"{Path.GetFileNameWithoutExtension(cisloDL)}-{dlId}.pdf";
                var fileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "PDF", rootFileName);
                var outputFileName = $"PDF/{rootFileName}";
                
                document.GeneratePdf(fileName);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
        #endregion

        #region Prace s cenou za zvěř
        /// <summary>
        /// Vrací cenu za zvěř dle kategorie a druhu
        /// </summary>
        /// <param name="cenik">Celá třída ceníku z DB</param>
        /// <param name="kategorie">id kategori</param>
        /// <param name="druh">id druhu</param>
        /// <returns></returns>
        public static decimal VratCenu(Ceniky cenik, int kategorie, int druh)
        {
            switch (druh)
            {
                case 1:
                    switch (kategorie)
                    {
                        case 1:
                            return cenik.DivocakI;
                        case 2:
                            return cenik.DivocakII;
                        case 3:
                            return cenik.DivocakIII;
                    }
                    break;
                case 2:
                    switch (kategorie)
                    {
                        case 1:
                            return cenik.SrnciI;
                        case 2:
                            return cenik.SrnciII;
                        case 3:
                            return cenik.SrnciIII;
                    }
                    break;
                case 3:
                    switch (kategorie)
                    {
                        case 1:
                            return cenik.JelenI;
                        case 2:
                            return cenik.JelenII;
                        case 3:
                            return cenik.JelenIII;
                    }
                    break;
                case 4:
                    switch (kategorie)
                    {
                        case 1:
                            return cenik.DanekI;
                        case 2:
                            return cenik.DanekII;
                        case 3:
                            return cenik.DanekIII;
                    }
                    break;
                case 5:
                    switch (kategorie)
                    {
                        case 1:
                            return cenik.SikaI;
                        case 2:
                            return cenik.SikaII;
                        case 3:
                            return cenik.SikaIII;
                    }
                    break;
                case 6:
                    switch (kategorie)
                    {
                        case 1:
                            return cenik.MuflonI;
                        case 2:
                            return cenik.MuflonII;
                        case 3:
                            return cenik.MuflonIII;
                    }
                    break;
            }
            return 0;
        }
        #endregion


        #region Report

        public static string ReportCesta(string nazevFRX) => $"wwwroot/Reports/{nazevFRX}";
        public static string ReportExportPDFCesta(string nazevPDF) => $"wwwroot/Reports/PDF/{nazevPDF}";
        public static string ReportPrintPDFCesta(string nazevPDF) => $"Reports/PDF/{nazevPDF}";


        #endregion




    }
}
