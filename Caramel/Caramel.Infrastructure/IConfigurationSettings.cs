namespace Caramel.Infrastructure
{
    public interface IConfigurationSettings
    {
        string JwtKey { get; }

        string Issuer { get; }

        // string WebSiteURl { get; }
    }
}
