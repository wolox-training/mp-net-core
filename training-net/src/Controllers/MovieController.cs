using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using training_net.Models.Views;
using training_net.Repositories.Database;
using training_net.Repositories.Interfaces;
using training_net.Models;
using System.Collections;

namespace training_net.Controllers
{
    [Route("Movie")]
    public class MovieController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHtmlLocalizer<MovieController> _localizer;
        private IList<Movie> _movieList;

        public MovieController(IUnitOfWork unitOfWork, IHtmlLocalizer<MovieController> localizer, IList<Movie> movieList)
        {
            this._unitOfWork = unitOfWork;
            this._localizer = localizer;
            this._movieList = movieList;
        }
        
        public IUnitOfWork UnitOfWork
        {
            get => this._unitOfWork;
        }

        public IList<Movie> MovieList
        {
            get => this._movieList;
            set => this._movieList = value;
        }  

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewData["Title"] = _localizer["MovieControllerTitle"].Value;
            ViewData["Paragraph"] = _localizer["MovieControllerParagraph"].Value;
            MovieList = UnitOfWork.MovieRepository.GetAll().ToList();      
            return View(MovieList);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewData["Title"] = _localizer["MovieCreateTitle"].Value;
            ViewData["Paragraph"] = _localizer["MovieCreateParagraph"].Value;      
            return View();
        }

        [HttpPost("Save")]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> OnPost([FromBody] MovieViewModel movie)
        {
            if (!ModelState.IsValid) 
                return View();

            UnitOfWork.MovieRepository.Add(new Movie {
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                Genre = movie.Genre,
                Price = movie.Price
            });
            _unitOfWork.Complete();
            return RedirectToAction("Index","Movie");
        }
    }
}
