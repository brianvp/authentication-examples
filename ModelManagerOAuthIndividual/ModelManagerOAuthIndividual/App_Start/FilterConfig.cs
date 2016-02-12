using System.Web;
using System.Web.Mvc;
using ModelManagerOAuthIndividual.Filters;

namespace ModelManagerOAuthIndividual
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //filters.Add(new AuthorizeRedirectAttribute());
        }
    }
}
