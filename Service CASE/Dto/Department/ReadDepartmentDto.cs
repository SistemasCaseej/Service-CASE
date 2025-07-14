using Service_CASE.Models.Department;

namespace Service_CASE.Models.Department;

public class ReadDepartmentDto
{
    public string? Id { get; set; }
    public string Name { get; set; }
    
    public ReadDepartmentDto(string id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public static ReadDepartmentDto FromModel(Department department)
    {
        return new ReadDepartmentDto(department.Id, department.Name);
    }
}