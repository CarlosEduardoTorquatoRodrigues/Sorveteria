using Microsoft.EntityFrameworkCore;
using Sorveteria.Application.Interfaces;
using Sorveteria.Application.Services;
using Sorveteria.Domain.Interfaces;
using Sorveteria.Infrastructure.Data;
using Sorveteria.Infrastructure.Repositories;
using Mapster;
using Sorveteria.Domain.Entities;
using Sorveteria.Application.ViewModels;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<SorveteriaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ISorveteRepository, SorveteRepository>();


builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ISorveteService, SorveteService>();


TypeAdapterConfig<Sorvete, SorveteViewModel>.NewConfig()
    .Map(dest => dest.CategoriaNome, src => src.Categoria != null ? src.Categoria.Nome : string.Empty);

TypeAdapterConfig<Categoria, CategoriaViewModel>.NewConfig()
    .Map(dest => dest.QuantidadeSorvetes, src => src.Sorvetes != null ? src.Sorvetes.Count : 0);

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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