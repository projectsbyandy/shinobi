namespace Shinobi.Core.Models.Config;

public record SqlConnectionDetails
{
    public string? Server { get; init; }
    public string? Catalog { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
}