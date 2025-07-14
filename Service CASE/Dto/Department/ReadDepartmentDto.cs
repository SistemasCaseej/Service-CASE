namespace Service_CASE.Models.Department;

public class ReadDepartmentDto
{
    public string? Id { get; set; }
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string Responsible { get; set; }
    
    public string ResponsibleEmail { get; set; }
    
    public ReadDepartmentDto(string id, string name, string description, string responsible, string responsibleEmail)
    {
        Id = id;
        Name = name;
        Description = description;
        Responsible = responsible;
        ResponsibleEmail = responsibleEmail;
    }
    
    public static ReadDepartmentDto FromModel(Department department)
    {
        return new ReadDepartmentDto(department.Id, department.Name,  department.Description, department.Responsible, department.ResponsibleEmail);
    }
}