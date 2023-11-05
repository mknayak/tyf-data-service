/// <summary>
/// The update user request.
/// </summary>
public class UpdateUserRequest
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    /// <value>The user ID.</value>
    /// <remarks>This field is required.</remarks>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>The name.</value>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    /// <value>The email.</value>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    /// <value>The password.</value>
    public string? Password { get; set; }
}
