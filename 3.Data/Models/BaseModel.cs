using System.Text.Json.Serialization;

namespace _3.Data.Models;

public class BaseModel
{
    public int Id { get; set; }
    [JsonIgnore]
    public Boolean IsActive { get; set; } = true;
    
}