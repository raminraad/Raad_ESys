using ESys.Application;
using ESys.Libraries;
using ESys.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


#region Add referenced projects services

var configuration = new ConfigurationBuilder().Build();
builder.Services.AddEPaaSApplication();
builder.Services.AddEPaasServices();
builder.Services.AddEPaaSPersistence(configuration);

#endregion

builder.Services.AddSession();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "epaas/{controller=Home}/{action=Index}/{id?}");

app.Run();