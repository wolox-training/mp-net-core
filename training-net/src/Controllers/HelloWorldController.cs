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

        public IHtmlLocalizer Localizer
        {
            get { return this._localizer; }
        }

        public IActionResult Index()
        {
            ViewData["Title"] = Localizer["HelloWorldTitle"].Value;            
            ViewData["Paragraph"] = Localizer["HelloWorldParagraph"].Value;
            return View();
        }
    }
}
