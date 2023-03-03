using AgendaDapperProcedimientos.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//Se añade como inyección de dependencias el repositorio (MUY IMPORTANTE)
// usando dapper
//builder.Services.AddScoped<IRepositorio, Repositorio>();

// usando dapper contrib
builder.Services.AddScoped<IRepositorio, RepositorioContrib>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=Index}/{id?}");

app.Run();
