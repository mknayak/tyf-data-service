namespace tyf.data.service.Models
{
    public class UserModel
    {
        public Guid UserId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public IEnumerable<string>? Roles { get; set; }
        public IEnumerable<string>? Namespaces { get; set; }
    }

    public class UserModelList : IListModel<UserModel>
    {
        public UserModelList(IEnumerable<UserModel> users)
        {
            this.Results = users;
        }
        public IEnumerable<UserModel> Results { get; set; }
        public int TotalResults { get; set; }
        public int Page { get; set; }
    }
}

