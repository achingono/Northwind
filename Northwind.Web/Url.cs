namespace Northwind.Web
{
    using System.Web.Routing;

    /// <summary>
    /// TODO: Update Summary
    /// </summary>
    public class Url
    {
        /// <summary>
        /// Generate a url to the specified <paramref name="controller"/>
        /// </summary>
        /// <param name="controller">The controller</param>
        /// <param name="id">Optional Id parameter</param>
        /// <returns>A string containing the url to the controller</returns>
        public static string ApiUrl(string controller, object id = null)
        {
            var routeValues = new RouteValueDictionary { { "httproute", "" }, { "controller", controller } };

            if (id != null)
            {
                routeValues.Add("id", id);
            }

            return RouteTable.Routes.GetVirtualPath(null, "Api", routeValues).VirtualPath;
        }

        /// <summary>
        /// Generate a url to the specified <paramref name="controller"/>
        /// </summary>
        /// <param name="controller">The controller</param>
        /// <param name="action">The action to execute</param>
        /// <param name="id">Optional Id parameter</param>
        /// <returns>A string containing the url to the controller</returns>
        public static string ApiUrl(string controller, string action, object id)
        {
            return RouteTable.Routes.GetVirtualPath(null, "ApiAction",
                new RouteValueDictionary { 
                    { "httproute", "" }, 
                    { "controller", controller } ,
                    { "action", action },
                    { "id", id }
                }).VirtualPath;
        }
    }
}