namespace tyf.data.service.Models
{
    public class InstanceListModel
    {
        public InstanceListModel()
        {
            Results = new List<SchemaInstanceModel>();
        }
        public IEnumerable<SchemaInstanceModel> Results { get; set; }
        public int TotalResults { get; set; }
        public int Page { get; set; }
    }
}



