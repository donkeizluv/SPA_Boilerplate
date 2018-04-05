using System;
using System.DirectoryServices.Protocols;

namespace DocumentArchiver.Auth
{
    static public class WindowsAuth
    {
        public static bool Validate_Principal(string userName, string pwd, string domain)
        {
            using (var pc = new System.DirectoryServices.AccountManagement
                .PrincipalContext(System.DirectoryServices.AccountManagement.ContextType.Domain, domain))
            {
                // validate the credentials
                return pc.ValidateCredentials(userName, pwd);
            }
        }
        //Does not work
        //public static bool Validate_Interop(string userName, string password, string domain)
        //{
        //    var tokenHandler = IntPtr.Zero;
        //    bool isValid = InteropAuth.LogonUser(userName, domain, password, 2, 0, ref tokenHandler);
        //    return isValid;
        //}

        //Pretty fast
        public static bool Validate_Principal2(string username, string password, string domain)
        {
            try
            {
                //string userdn;
                using (var lconn = new LdapConnection(new LdapDirectoryIdentifier(domain)))
                {
                    lconn.Bind(new System.Net.NetworkCredential(username, password, domain));
                    return true;
                }
            }
            catch (LdapException e)
            {
                return false;
            }
        }
    }
}
