using System.Linq;
using Microsoft.AspNetCore.Mvc;
using training_net.Models.Views;
using training_net.Repositories.Interfaces;
using training_net.Models;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult Index() => View(UnitOfWork.MovieRepository.GetAll().Select(
            movie => new MovieViewModel {
                ID = movie.ID,
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                Genre = movie.Genre,
                Price = movie.Price
            }
        ).ToList());

        [HttpGet("Create")]
        public IActionResult Create() => View();

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] MovieViewModel movie)
        {
            if (!ModelState.IsValid) 
                return RedirectToAction("Create","Movie");
            UnitOfWork.MovieRepository.Add(new Movie {
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                Genre = movie.Genre,
                Price = movie.Price
            });
            UnitOfWork.Complete();
            return RedirectToAction("Index","Movie");
        }

        [HttpGet("Edit")]
        public IActionResult Edit(int? id)
        {
            if(id == null)
                return NotFound();
            
            MovieViewModel model = new MovieViewModel();
            var movie = UnitOfWork.MovieRepository.Get(id.Value);
            if(movie == null)
                return NotFound();
            model.ID = movie.ID;
            model.Genre = movie.Genre;
            model.Price = movie.Price;
            model.ReleaseDate = movie.ReleaseDate;
            model.Title = movie.Title;
            return View(model);
        }
        
        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [FromForm] MovieViewModel movie)
        {
            var temp = UnitOfWork.MovieRepository.Get(id);
            if(ModelState.IsValid)
            {
                try
                {
                    temp.ID = id;
                    temp.Genre = movie.Genre;
                    temp.Price = movie.Price;
                    temp.ReleaseDate = movie.ReleaseDate;
                    temp.Title = movie.Title;
                    UnitOfWork.MovieRepository.Update(temp);
                    UnitOfWork.Complete();
                }
                catch(DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction("Index","Movie");
            }
            return RedirectToAction("Edit","Movie", new{ID=id});
        }
    }
}
