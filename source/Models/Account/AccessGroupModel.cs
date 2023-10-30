namespace tyf.data.service.Models.Account
{
    public class AccessGroupModel
    {
        public required Guid GroupId { get; set; }
        public required string Name { get; set; }
        public required string Namespace { get; set; }
    }
    public class AccessGroupModelList : IListModel<AccessGroupModel>
    {
        public AccessGroupModelList()
        {
            Results = new List<AccessGroupModel>();
        }
        public IEnumerable<AccessGroupModel> Results { get; set; }
        public int TotalResults { get; set; }
        public int Page { get; set; }
    }
}

