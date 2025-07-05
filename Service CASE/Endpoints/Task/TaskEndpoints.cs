using Microsoft.EntityFrameworkCore;
using Service_CASE.Data;
using Service_CASE.Models.Task;
using Service_CASE.Services;

namespace Service_CASE.Endpoints.Task;

public static class TaskEndpoints
{
    public static void MapTaskEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/task", async (UserTaskService service) =>
        {
            var tasks = await service.GetUsersAsync();
            return Results.Ok(tasks);
        });
        
        app.MapGet("/debug/db", async (AppDbContext db) =>
        {
            var tasks = await db.UserTasks.ToListAsync();
            return Results.Ok(tasks);
        });
    }
}