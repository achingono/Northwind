namespace Northwind.Web
{
    using System;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Profile;
    using System.Web.Security;

    public static class IPrincipalExtensions
    {
        /// <summary>
        /// Determine if the specified <paramref name="user"/> is authenticated
        /// </summary>
        /// <param name="user">The <see cref="IPrincipal"/> to verify</param>
        /// <returns>TRUE if the <paramref name="user"/> is authenticated</returns>
        public static bool IsAuthenticated(this IPrincipal user)
        {
            bool result = false;

            if (user != null
                && user.Identity != null
                && !string.IsNullOrEmpty(user.Identity.Name))
            {
                ProfileBase p = ProfileBase.Create(user.Identity.Name);
                result = (user.Identity.IsAuthenticated
                && !p.IsAnonymous);
            }
            return result;
        }

        /// <summary>
        /// Get the name of the specified <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns></returns>
        public static string Name(this IPrincipal user)
        {
            string username = "";

            if (user.IsAuthenticated())
            {
                username = user.Identity.Name;
            }
            return username;
        }

        /// <summary>
        /// Get the email of the specified <paramref name="user"/>.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string Email(this IPrincipal user)
        {
            string email = string.Empty;
            var member = Membership.GetUser(user.Identity.Name);
            if (member != null)
                email = member.Email;
            return email;
        }

        /// <summary>
        /// Get the IP address of the specified <paramref name="user"/>.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string IP(this IPrincipal user)
        {
            string address = "127.0.0.1";
            var context = HttpContext.Current;
            if (context != null
                && context.Request != null)
            {
                try
                {
                    address = context.Request.UserHostAddress;
                }
                catch (NullReferenceException) { }
            }
            return address;
        }

        /// <summary>
        /// Determine if the specified <paramref name="user"/> is an administrator
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool IsAdministrator(this IPrincipal user)
        {
            return (user.IsAuthenticated() && user.IsInRole("Administrators"));
        }
    }
}
