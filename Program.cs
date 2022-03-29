using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using e_commerce.Datas;
using e_commerce.Interface;
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

#region  Business Services Injection

builder.Services.AddScoped<IProdukService, ProdukService>();
builder.Services.AddScoped<IKategoriService, KategoriService>();

#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
