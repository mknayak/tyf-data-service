/// <summary>
/// Represents a request to search for a schema.
/// </summary>
public class SearchSchemaRequest
{
    /// <summary>
    /// Gets or sets the name of the schema to search for.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the namespace of the schema to search for.
    /// </summary>
    public string? Namespace { get; set; }

    /// <summary>
    /// Gets or sets the page number of the search results to retrieve.
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// Gets or sets the number of search results to retrieve per page.
    /// </summary>
    public int PageSize { get; set; }
}