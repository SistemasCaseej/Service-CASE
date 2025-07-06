using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service_CASE.Data;
using Service_CASE.Models.Task;
using Service_CASE.Services;

namespace Service_CASE.Endpoints.Task;

public static class TaskEndpoints
{
    public static void MapTaskEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/tasks", async (UserTaskService service) =>
        {
            var tasks = await service.GetUsersAsync();
            return Results.Ok(tasks);
        });

        app.MapGet("/debug/db", async (AppDbContext db) =>
        {
            var tasks = await db.UserTasks.ToListAsync();
            return Results.Ok(tasks);
        });

        app.MapPost("/add/task", async (UserTask task, UserTaskService service) =>
        {
            var createdTask = await service.CreateTaskAsync(task);
            return Results.Created($"/task/{createdTask.Id}", createdTask);
        });

        app.MapPut("/task/{id:int}", async (int id, UserTask input, UserTaskService service) =>
        {
            var updated = await service.UpdateTaskAsync(id, input);
            return updated is not null ? Results.Ok(updated) : Results.NotFound();
        });

        app.MapDelete("/task/{id:int}", async (int id, UserTaskService service) =>
        {
                var deleted = await service.DeleteTaskAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound(); 
        });

        }
}