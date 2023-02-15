//using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Configuracion para la autenticacion 
/*builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Acceso/LoginAdulto";
        option.ExpireTimeSpan= TimeSpan.FromMinutes(20);
        //Vistas para que no ingrese al entrar como usuario
        //Vista a la que se va a rediriguir cuando no tenga acceso a una vista

        //option.AccessDeniedPath = "/Acceso/ContenidoA";

    });*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=LoginAdulto}/{id?}");

app.Run();
 