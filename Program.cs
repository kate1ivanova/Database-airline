using _3LabaCSharp.Migrations; // ���������, ��� ���� namespace ���������
using _3LabaCSharp.Models; // ���������, ��� ���� namespace ���������
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// �������� ������ ����������� �� ����� ������������
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

//  �������� AirlineContext � �������� ������� � ����������
builder.Services.AddDbContext<AirlineContext>(opt => opt.UseSqlServer(connection));

// ��������� JsonSerializerOptions ��� ��������� ����������� ������
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});

builder.Services.AddScoped(typeof(TodoDbStorage<>));
builder.Services.AddControllers();
// ��������� Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ���������� �������� ��� ������� ����������
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AirlineContext>();
    dbContext.Database.EnsureCreated(); // ������� ���� ������, ���� ��� �� ����������
    // ��������� ��� ��������, ������� ��� �� ���� ���������
    // dbContext.Database.Migrate(); // ���� ������ ��������� ��������, ���������������� ��� ������
}

// ��������� Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ��������� ������ middleware
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// ������ ����������
app.Run();
