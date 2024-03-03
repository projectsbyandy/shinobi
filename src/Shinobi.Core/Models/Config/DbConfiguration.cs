namespace Shinobi.Core.Models.Config;

public record DbConfiguration
{
    public bool UseMock { get; init; }
    public SqlConnectionDetails? SqlConnectionDetails { get; init; }
}