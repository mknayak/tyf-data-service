namespace tyf.data.service.Models;

public class SchemaInstanceModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required Dictionary<string, string> Fields { get; set; }
}



