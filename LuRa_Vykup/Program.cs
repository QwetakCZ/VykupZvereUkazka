using LuRa_Vykup.Areas.Identity;
using LuRa_Vykup.Controllers;
using LuRa_Vykup.Areas.Identity.Pages.Account;
using LuRa_Vykup.Areas.Identity.Pages.Account.Manage;
using LuRa_Vykup.Data;
using LuRa_Vykup.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Blazored.LocalStorage;
using LuRa_Vykup.Services.Interface;
using Append.Blazor.Printing;
using DeviceType;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
                        {
                            //Uprava pozadavku na potvrzeni uctu
                            options.SignIn.RequireConfirmedAccount = false;
                            //Uprava pozadavku na heslo - Neni potreba specialni znaky
                            options.Password.RequireNonAlphanumeric = false;
                            //Uprava pozadavku na heslo - Neni potreba velka pismena
                            options.Password.RequireUppercase = false;
                            //Uprava pozadavku na heslo - Neni potreba cislice
                            options.Password.RequireDigit = false;
                        })
   .AddRoles<IdentityRole>()
   .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddBlazorBootstrap();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddBlazorCurrentDevice();

builder.Services.AddBlazoredLocalStorage(config =>
        config.JsonSerializerOptions.WriteIndented = true);

builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
//builder.Services.AddSingleton<WeatherForecastService>();


//Dependency Injection - musi se pridat modely i stranky co chces dat do dependence
builder.Services.AddTransient<ApplicationDbContext>();
builder.Services.AddTransient<DodavateleController>();
//builder.Services.AddTransient<LoginServices>();
builder.Services.AddTransient<VykupController>();
builder.Services.AddTransient<DLController>();
builder.Services.AddTransient<VykupModel>();
builder.Services.AddTransient<RegisterModel>();
builder.Services.AddTransient<CreatePdfFromDL>();
builder.Services.AddTransient<CenikyController>();
builder.Services.AddTransient<ILocalStorage, LocalStorage>();
builder.Services.AddTransient<PokladniDokladyController>();
builder.Services.AddTransient<TracesController>();

//builder.Services.AddScoped<IBlazorCurrentDeviceService, BlazorCurrentDeviceService>();

builder.Services.AddScoped<IPrintingService, PrintingService>();

//builder.Services.AddFastReport();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
//app.UseFastReport();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
