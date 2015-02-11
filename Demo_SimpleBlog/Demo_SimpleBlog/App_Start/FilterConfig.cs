using System.Web.Mvc;
using Demo_SimpleBlog.Infrastructure;

namespace Demo_SimpleBlog.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new TransactionFilter());
        }
    }
}