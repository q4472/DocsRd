using Microsoft.AspNetCore.Mvc;
using System;

namespace DocsRd.Controllers
{
    public class RdController : Controller
    {
        public Object Index()
        {
            Object result = "DocsRd.Controllers.HomeController.Rd()<br />";
            try
            {
                Guid.TryParse(Request.Form["sessionId"], out Guid sessionId);
                result = PartialView(sessionId);
            }
            catch (Exception e) { result += e.ToString(); }
            return result;
        }
    }
}
