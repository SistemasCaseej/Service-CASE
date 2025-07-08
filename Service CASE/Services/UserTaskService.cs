using MongoDB.Driver;
using Service_CASE.Models.Task;

namespace Service_CASE.Services;

public class UserTaskService
{
    private readonly IMongoCollection<UserTask> _collection;

    public UserTaskService()
    {
        var connectionString = Environment.GetEnvironmentVariable("Connection_String");
        
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("A variável de ambiente CONNECTION_STRING não foi encontrada ou está vazia.");
        }
        
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("sistema_case");
        _collection = database.GetCollection<UserTask>("tasks");
    }

    public async Task<List<UserTask>> GetAllTaskAsync()
    {
       return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<UserTask?> GetByIdAsync(string id)
    {
        return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<UserTask?> CreateTaskAsync(UserTask task)
    {
        await _collection.InsertOneAsync(task);
        return task;
    }
}