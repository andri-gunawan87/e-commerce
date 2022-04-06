using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using e_commerce.Datas;
using e_commerce.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using e_commerce.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ecommerceContext>(
            dbContextOptions => dbContextOptions
                .UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), 
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")))
                // The following three options help with debugging, but should
                // be changed or removed for production.
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
        );

// Add services to the container.
#region  Business Services Injection
builder.Services.AddScoped<IProdukService, ProdukService>();
builder.Services.AddScoped<IKategoriService, KategoriService>();
builder.Services.AddScoped<IProdukKategoriService, ProdukKategoriService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IkeranjangService, KeranjangService>();
builder.Services.AddScoped<IAlamatService, AlamatService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IStatusService, StatusService>();

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(
        options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromDays(30);
            options.SlidingExpiration = true;
            options.AccessDeniedPath = "/Home/Privacy";
            options.LoginPath = "/Auth/Login";
        });
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
