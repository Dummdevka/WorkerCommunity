using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application;
using Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.addApplication();
builder.Services.addInfrastructure(builder.Configuration, builder.Host);

//Caching
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "WorkCommunity_";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

