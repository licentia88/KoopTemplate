using System.DirectoryServices.Protocols;
using System.Net;

namespace KoopTemplate.Web.Authentication;

public sealed class AdAuthenticator : IAdAuthenticator
{
    private readonly string _domain;
    private readonly string _containerOrServer;

    public string LdapServer { get; set; }

    public AdAuthenticator(string domain, string containerOrServer = null)
    {
        _domain = domain;
        _containerOrServer = containerOrServer;
    }

    public bool Validate(string username, string password , string ldapServer)
    {
        try
        {
            using var connection = new LdapConnection(ldapServer);
            connection.AuthType = AuthType.Basic;
            var credential = new NetworkCredential($"{ldapServer}\\{username}", password);
            connection.Bind(credential);

            return true;
        }
        catch (LdapException ex)
        {
            //  _logger.LogError("LDAP Authentication Error: {Message}", ex.Message);
            return false;
        }
    }

    //public bool Validate(string username, string password)
    //{
    //    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
    //        return false;

    //    var user = username.Contains('\\') || username.Contains('@')
    //        ? username
    //        : $"{_domain}\\{username}";

    //    try
    //    {
    //        using var context = _containerOrServer is null
    //            ? new PrincipalContext(ContextType.Domain, _domain)
    //            : new PrincipalContext(ContextType.Domain, _domain, _containerOrServer);

    //        return context.ValidateCredentials(user, password, ContextOptions.Negotiate);
    //    }
    //    catch
    //    {
    //        return false;
    //    }
    //}
}