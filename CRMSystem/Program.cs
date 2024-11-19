using Microsoft.EntityFrameworkCore;
using CRMSystem.Db;
using CRMSystem.Models;
using CRMSystem.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CRMSystem.Services.Implementations;
using CRMSystem.Services.Interfaces;
using CRMSystem.Repository.Implementations;
using CRMSystem.Repository.Interfaces;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetSection("Data:ManagementDB:ConnectionString").Value;
var connectionStringIdentity = builder.Configuration.GetSection("Data:CRMIdentity:ConnectionString").Value;

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<AppIdentityContext>(options => options.UseSqlServer(connectionStringIdentity));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityContext>()
    .AddDefaultTokenProviders();

builder.Services.AddTransient<IClientService, ClientService>();
builder.Services.AddTransient<IClientRepository, ClientRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

SeedData.EnsurePopulated(app);
await IdentitySeedData.EnsurePopulatedAsync(app);

app.UseDeveloperExceptionPage();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "account",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "manager",
    pattern: "{controller=Manager}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "admin",
    pattern: "{controller=Admin}/{action=Index}/{id?}");

app.Run();