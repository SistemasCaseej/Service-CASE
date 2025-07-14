using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Service_CASE.Models.Task;

public class UserTask
{   
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } 
    public string title { get; set; }
    public string description { get; set; } 
    //public DateOnly deadLine { get; set; }
    public string responsible {  get; set; } //Trocar pela a entidade usuário depois
    public string department {  get; set; } //Troca para a entidade Departamento depois
    public string createdBy  { get; set; }
    
    [BsonRepresentation(BsonType.String)]
    public Status status { get; set; }

    public enum Status
    {
        Todo, 
        InProgress, 
        Done
    }
}