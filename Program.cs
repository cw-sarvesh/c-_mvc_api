
using faqRepository;
using FAQModels;
using System;
using Microsoft.AspNetCore.Mvc;
using Consul;
using System.Text;
using AEPLCore.DataAccess;
using AEPLCore.DataAccess.Contracts;
using AEPLCore.Monitoring;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using UsedCar.Interface.CertificationReport;

using AEPLCore.Utils.KMSAccess;


using Usedcarapi.FaqData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();





string consulHost = Environment.GetEnvironmentVariable("CONSUL_HOST") ?? "10.10.20.113";// Configure the HTTP request pipeline.
builder.Services.AddSingleton<IFAQData, FAQRespository>();
builder.Services.AddSingleton<ICertificationReport, CertificationRepository>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddDbConnectionString<ConnectionStringOption, IOptions<ConnectionStringOption>>(builder.Configuration);

builder.Services.AddSingleton<ConnectionFactory<IOptions<ConnectionStringOption>>>();
builder.Services.AddMonitoring(consulHost + ":8500", builder.Configuration["ServiceSettings:ModuleName"]);

builder.Services.Configure((Action<KMSAccessConfigOptions>)(options => builder.Configuration.GetSection("KMSAccessKeys").Bind(options)));
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}
else
{

    if (Environment.GetEnvironmentVariable("IS_IN_AWS") == null)
    {
        using (ConsulClient _client = new ConsulClient(c => c.Address = new Uri("http://" + consulHost + ":8500")))
        {
            string consulKey = "KMSAccessKeys/dev/";
            KVPair[] kmsKeyList = _client.KV.List(consulKey).Result.Response;
            if (kmsKeyList != null)
            {
                var accessPair = kmsKeyList.FirstOrDefault(x => x.Key.Equals(String.Concat(consulKey, "KMSAccessKey")));
                var secretAccessPair = kmsKeyList.FirstOrDefault(x => x.Key.Equals(String.Concat(consulKey, "KMSAccessSecret")));
                if (accessPair != null && accessPair.Value != null && secretAccessPair != null && secretAccessPair.Value != null)
                {
                    app.Configuration["KMSAccessKeys:KMSAccessKey"] = Encoding.UTF8.GetString(accessPair.Value);
                    app.Configuration["KMSAccessKeys:KMSAccessSecret"] = Encoding.UTF8.GetString(secretAccessPair.Value);
                }
                else
                {
                    throw new ArgumentException("KMSKey cannot be null. Couldn't read KMSKey from Consul.");
                }
            }
        }
    }

}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
