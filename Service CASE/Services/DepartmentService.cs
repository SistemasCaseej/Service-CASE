using MongoDB.Driver;
using Service_CASE.Dto;
using Service_CASE.Models.Department;

namespace Service_CASE.Services;

public class DepartmentService
{
    private readonly IMongoCollection<Department> _collection;

    public DepartmentService()
    {
        var connectionString = Environment.GetEnvironmentVariable("Connection_String");
        
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("A variável de ambiente CONNECTION_STRING não foi encontrada ou está vazia.");
        }
        
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("sistema_case");
        _collection = database.GetCollection<Department>("departments");
    }

    public async Task<List<ReadDepartmentDto>> GetAllDepartments()
    {
        var departments = await _collection.Find(_ => true).ToListAsync();
        
        var readDepartmentDtos = departments.Select(ReadDepartmentDto.FromModel).ToList();

        return readDepartmentDtos;
    }
    
    public async Task<ReadDepartmentDto> CreateDepartment(CreateDepartmentDto departmentDto)
    {
        var department = Department.FromCreateDto(departmentDto);
        
        await _collection.InsertOneAsync(department);
        
        return ReadDepartmentDto.FromModel(department);
    }

    public async Task<ReadDepartmentDto> DeleteDepartment(string id)
    {
        var department = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        
        if (department == null)
            return null;

        await _collection.DeleteOneAsync(x => x.Id == id);
        
        return ReadDepartmentDto.FromModel(department);
    }
}