using System;

namespace tyf.data.service.Models
{
    public class SchemaListModel : IListModel<SchemaModel>
    {
        public SchemaListModel()
        {
            Results = new List<SchemaModel>();
        }
        public IEnumerable<SchemaModel> Results { get; set; }
        public int TotalResults { get; set; }
        public int Page { get; set; }
    }
}



