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
    
    public async Task<List<UserTask>> GetUsersAsync() =>
        await _context.UserTasks.ToListAsync();

}