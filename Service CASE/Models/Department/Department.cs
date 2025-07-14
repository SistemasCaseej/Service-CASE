using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Service_CASE.Dto;

namespace Service_CASE.Models.Department;

public class Department
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> Members { get; set; } = new();
    public string Responsible { get; set; }
    public string ResponsibleEmail { get; set; }

    public Department(string name, string description, string responsible, string responsibleEmail)
    {
        Name = name;
        Description = description;
        Responsible = responsible;
        ResponsibleEmail = responsibleEmail;
    }
    
    public static Department FromCreateDto(CreateDepartmentDto dto)
    {
        return new Department(dto.Name, dto.Description, dto.Responsible, dto.ResponsibleEmail);
    }

}