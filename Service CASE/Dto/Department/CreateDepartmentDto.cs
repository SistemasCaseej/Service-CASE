using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Service_CASE.Dto;

public class CreateDepartmentDto
{
    public string? Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public List<string> Members { get; set; } = new();
    
    [Required]
    public string Responsible { get; set; }
    
    [Required]
    public string ResponsibleEmail { get; set; }
}