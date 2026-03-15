var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Controller ekle
builder.Services.AddOpenApi();    // Swagger/OpenAPI (opsiyonel)

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// **Statik dosyaları servis et ve index.html'i varsayılan aç**
app.UseDefaultFiles();   // / yazınca index.html açılır
app.UseStaticFiles();    // wwwroot içindeki CSS/JS/HTML dosyaları çalışır

//app.UseHttpsRedirection();

app.MapControllers();    // API controller’ları map et

app.Run();