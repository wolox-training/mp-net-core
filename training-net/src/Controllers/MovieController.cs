using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using training_net.Models.Views;
using training_net.Repositories.Database;
using training_net.Repositories.Interfaces;

namespace training_net.Controllers
{
    public class MovieController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHtmlLocalizer<MovieController> _localizer;
        public MovieController(IUnitOfWork unitOfWork, IHtmlLocalizer<MovieController> localizer)
        {
            this._unitOfWork = unitOfWork;
            this._localizer = localizer;
        }

        public IUnitOfWork UnitOfWork
        {
            get { return this._unitOfWork; }
        }

        public IActionResult Index()
        {
            //ViewData["indexMovieTitle"] = localizer;
            //@ViewData["indexMoviePar"] = ;      
            return View();
        }
        public IActionResult Create()
        {
                              
            return View();
        }
    }
}
