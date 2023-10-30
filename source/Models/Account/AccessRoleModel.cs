using System;

namespace tyf.data.service.Models.Account
{
    public class AccessRoleModel
    {
        public required Guid RoleId { get; set; }
        public required string Name { get; set; }
    }
    public class AccessRoleModelLst : IListModel<AccessRoleModel>
    {
        public AccessRoleModelLst()
        {
            Results = new List<AccessRoleModel>();
        }
        public IEnumerable<AccessRoleModel> Results { get; set; }
        public int TotalResults { get; set; }
        public int Page { get; set; }
    }

}

