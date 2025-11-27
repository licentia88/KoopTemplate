namespace KoopTemplate.Web.Models;

public interface IAdAuthenticator
{
    public string LdapServer { get; set; }
    bool Validate(string username, string password, string ldapServer);
}