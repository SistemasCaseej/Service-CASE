using Microsoft.EntityFrameworkCore;
using Service_CASE.Data;
using Service_CASE.Endpoints.Task;
using Service_CASE.Models.Task;
using Service_CASE.Services;
using Service_CASE.Swagger;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseInMemoryDatabase("Service API");
});

builder.Services.AddScoped<UserTaskService>();
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.UserTasks.AddRange(new UserTask 
        {  title = "Criar um post no instagram", description = "Criar um post", responsible = "José Vitor", department = "HR", createdBy = "José", Id = 20});
    db.SaveChanges();
}

app.UseSwaggerDocumentation();
app.MapTaskEndpoints();

app.Run();