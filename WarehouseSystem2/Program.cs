using WarehouseSystem.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
});

var connStr = builder.Configuration.GetConnectionString("WarehouseDB");
builder.Services.AddScoped<WarehouseRepository>(_ => new WarehouseRepository(connStr!));

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();