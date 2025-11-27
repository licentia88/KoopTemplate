
using KoopTemplate.Repository.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // This adds Swagger support

// Register Koop services
builder.Services.RegisterRepository();
builder.Services.AutoRegisterFromKoopTemplateRepository();

var app = builder.Build();
 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    app.UseSwagger(); // Swagger API belgesini etkinleştir
    app.UseSwaggerUI(c =>
    {
        // Swagger UI'yi doğru URL'de açılacak şekilde yapılandırıyoruz
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"); // API'nin swagger.json dosyasına erişim
        c.RoutePrefix = string.Empty; // Bu ayar Swagger UI'yi root (https://localhost:7126/) olarak açar
    });
}
 
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();