namespace Caramel.Infrastructure
{
    public interface IConfigurationSettings
    {
        string JwtKey { get; }

        string Issuar { get; }

        // string WebSiteURl { get; }
    }
}
