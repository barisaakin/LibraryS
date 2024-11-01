using Microsoft.EntityFrameworkCore;
using UdemyKitapSitesi.Models;
using UdemyKitapSitesi.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<UygulamaDbContext>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole >(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<UygulamaDbContext>().AddDefaultTokenProviders();

//Authentication kullanabimek için ef identity kütüphanesini ekledikten sonra bu servisi ekliyoruz
builder.Services.AddRazorPages();

// DIKKAT: YEN� B�R REPOSITORY SINIFI OLU�TURULDU�UNDA BURADA SERVICES A EKLENMELI
//controllerdaki _kitapTuruRepository nesnesinin olu�turulmas�n� sa�lar DI ile
builder.Services.AddScoped<IKitapTuruRepository, KitapTuruRepository>();
builder.Services.AddScoped<IKitapRepository, KitapRepository>();
builder.Services.AddScoped<IKiralamaRepository, KiralamaRepository>();

builder.Services.AddScoped<IEmailSender, EmailSender>();

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

//önemli 
app.MapRazorPages();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
