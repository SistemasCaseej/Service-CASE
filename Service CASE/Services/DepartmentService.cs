using MongoDB.Driver;
using Service_CASE.Dto;
using Service_CASE.Dto.Department;
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

    public async Task<ReadDepartmentDto?> GetDepartmentById(string id)
    {
        var department = await _collection.Find(department => department.Id == id).FirstOrDefaultAsync();
        
        if (department == null)
            return null;

        var readSingleDepartament = ReadDepartmentDto.FromModel(department);
        
        return readSingleDepartament;
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

    public async Task<ReadDepartmentDto?> UpdateDepartment(string id, UpdateDepartmentDto departmentDto)
    {
        var existing = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        
        if (existing == null)
            return null;
        
        if (!string.IsNullOrWhiteSpace(departmentDto.Name))
            existing.Name = departmentDto.Name;

        if (!string.IsNullOrWhiteSpace(departmentDto.Description))
            existing.Description = departmentDto.Description;

        if (!string.IsNullOrWhiteSpace(departmentDto.Responsible))
            existing.Responsible = departmentDto.Responsible;

        if (!string.IsNullOrWhiteSpace(departmentDto.ResponsibleEmail))
            existing.ResponsibleEmail = departmentDto.ResponsibleEmail;
        
        await _collection.ReplaceOneAsync(x => x.Id == id, existing);
        
        return ReadDepartmentDto.FromModel(existing);

    }

    public async Task<ReadDepartmentDto> DeleteDepartment(string id)
    {
        var department = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        
        if (department == null)
            throw new KeyNotFoundException("Departamento não encontrado.");

        await _collection.DeleteOneAsync(x => x.Id == id);
        
        return ReadDepartmentDto.FromModel(department);
    }
}