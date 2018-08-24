using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;

namespace training_net.Controllers
{
    public class HelloWorldController : Controller
    {
        private readonly IHtmlLocalizer<HelloWorldController> _localizer;
        public HelloWorldController(IHtmlLocalizer<HelloWorldController> localizer)
        {
            this._localizer = localizer;
        }
        public IActionResult Index()
        {
            ViewData["Title"] = _localizer["HelloWorldTitle"].Value;            
            ViewData["Paragraph"] = _localizer["HelloWorldParagraph"].Value;
            return View();
        }
    }
}
