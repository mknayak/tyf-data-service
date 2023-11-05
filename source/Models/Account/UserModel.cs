namespace tyf.data.service.Models
{
    /// <summary>
    /// Represents a user's information.
    /// </summary>
    public class UserInfo{
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Gets or sets the user's name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        public string Email { get; set; }
    }
    /// <summary>
    /// Represents a user in the system.
    /// </summary>
    public class UserModel : UserInfo
    {
        /// <summary>
        /// Gets or sets the roles associated with the user.
        /// </summary>
        public IEnumerable<string>? Roles { get; set; }

        /// <summary>
        /// Gets or sets the namespaces associated with the user.
        /// </summary>
        public IEnumerable<string>? Namespaces { get; set; }
    }

    /// <summary>
    /// Represents a list of user information.
    /// </summary>
    public class UserInfoList : IListModel<UserInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserInfoList"/> class.
        /// </summary>
        /// <param name="users">The list of user information.</param>
        public UserInfoList(IEnumerable<UserInfo> users)
        {
            this.Results = users;
        }

        /// <summary>
        /// Gets or sets the list of user information.
        /// </summary>
        public IEnumerable<UserInfo> Results { get; set; }

        /// <summary>
        /// Gets or sets the total number of results.
        /// </summary>
        public int TotalResults { get; set; }

        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        public int Page { get; set; }
    }
}

