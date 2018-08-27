using System.Linq;
using Microsoft.AspNetCore.Mvc;
using training_net.Models.Views;
using training_net.Repositories.Interfaces;
using training_net.Models;

namespace training_net.Controllers
{
    [Route("Movie")]
    public class MovieController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public MovieController(IUnitOfWork unitOfWork) => this._unitOfWork = unitOfWork;

        public IUnitOfWork UnitOfWork
        {
            get => this._unitOfWork;
        }

        [HttpGet("")]
        public IActionResult Index() => View(UnitOfWork.MovieRepository.GetAll().ToList());

        [HttpGet("Create")]
        public IActionResult Create() => View();

        [HttpPost("Save")]
        [ValidateAntiForgeryToken]
        public IActionResult OnPost([FromForm] MovieViewModel movie)
        {
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
