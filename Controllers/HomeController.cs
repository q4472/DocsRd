using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DocsRd.Models;

namespace DocsRd.Controllers
{
    public class HomeController : Controller
    {
        public Object Index()
        {
            // Заход при отладке. Метод GET. Параметров нет.
            Guid sessionId = new Guid("12345678-1234-1234-1234-123456789012");
            return View(sessionId);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
