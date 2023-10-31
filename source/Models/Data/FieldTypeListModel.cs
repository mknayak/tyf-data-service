namespace tyf.data.service.Models;

public class FieldTypeListModel
{
    public FieldTypeListModel()
    {
        Results = new List<FieldTypeModel>();
    }
    public IEnumerable<FieldTypeModel> Results { get; set; }
    public int TotalResults { get; set; }
    public int Page { get; set; }
}



