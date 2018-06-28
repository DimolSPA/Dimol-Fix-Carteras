using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jqGrid.Sample.Filters
{
    public class ErrorHandler: HandleErrorAttribute
{
    public override void OnException(ExceptionContext filterContext)
    {    
        base.OnException(filterContext);

        filterContext.HttpContext.Response.StatusCode = 200;
    }
}
}