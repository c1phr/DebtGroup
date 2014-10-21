using System.Web;
using System.Web.Mvc;

namespace DebtGroup
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //TODO: Uncomment the following to make auth take affect
            //filters.Add(new AuthorizeAttribute());
        }
    }
}