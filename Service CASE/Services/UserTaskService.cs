using Microsoft.EntityFrameworkCore;
using Service_CASE.Data;
using Service_CASE.Models.Task;

namespace Service_CASE.Services;

public class UserTaskService
{
    private readonly AppDbContext _context;

    public UserTaskService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<UserTask> CreateTaskAsync(UserTask userTask)
    {
     _context.UserTasks.Add(userTask);
     await _context.SaveChangesAsync();
     return userTask;
    }

    public async Task<UserTask?> UpdateTaskAsync(int id, UserTask updatedTask)
    {
        var existingTask = await _context.UserTasks.FindAsync(id);
        if (existingTask == null)
            return null;
        
        existingTask.title = updatedTask.title;
        existingTask.description = updatedTask.description;
        existingTask.responsible = updatedTask.responsible;
        existingTask.department = updatedTask.department;
        existingTask.createdBy = updatedTask.createdBy;
        
        await _context.SaveChangesAsync();
        return existingTask;
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        var task = await _context.UserTasks.FindAsync(id);
        if (task == null)
            return false;
        
        _context.UserTasks.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<List<UserTask>> GetUsersAsync() =>
        await _context.UserTasks.ToListAsync();
}