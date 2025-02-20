using _3LabaCSharp.Migrations; // Убедитесь, что этот namespace корректен
using _3LabaCSharp.Models; // Убедитесь, что этот namespace корректен
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Получаем строку подключения из файла конфигурации
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

//  контекст AirlineContext в качестве сервиса в приложение
builder.Services.AddDbContext<AirlineContext>(opt => opt.UseSqlServer(connection));

// Настройка JsonSerializerOptions для обработки циклических ссылок
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});

builder.Services.AddScoped(typeof(TodoDbStorage<>));
builder.Services.AddControllers();
// Добавляем Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Применение миграций при запуске приложения
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AirlineContext>();
    dbContext.Database.EnsureCreated(); // Создает базу данных, если она не существует
    // Применяет все миграции, которые еще не были применены
    // dbContext.Database.Migrate(); // Если хотите применить миграции, раскомментируйте эту строку
}

// Настройка Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Настройка других middleware
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Запуск приложения
app.Run();
