using Service_CASE.Services;
using Service_CASE.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<UserTaskService>();
builder.Services.AddSingleton<DepartmentService>();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddControllers();

var app = builder.Build();

app.UseSwaggerDocumentation();
app.MapControllers(); // ⬅️ NECESSÁRIO para que os Controllers funcionem

app.Run();