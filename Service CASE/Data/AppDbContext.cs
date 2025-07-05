using Microsoft.EntityFrameworkCore;
using Service_CASE.Models.Task;

namespace Service_CASE.Data;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    public DbSet<UserTask> UserTasks { get; set; }
}