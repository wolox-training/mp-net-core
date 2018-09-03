using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using training_net.Models.Views;
using training_net.Repositories.Interfaces;
using training_net.Models;
using Microsoft.AspNetCore.Authorization;

namespace training_net.Controllers
{
    [Authorize]
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
            movie => new MovieViewModel
            {
                Id = movie.Id,
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
        public IActionResult Create([FromForm] MovieViewModel movieVM)
        {
            try
            {
                ModelState.Remove("Id");
                if (!ModelState.IsValid)
                    return RedirectToAction("Create", "Movie");
                UnitOfWork.MovieRepository.Add(new Movie
                {
                    Title = movieVM.Title,
                    ReleaseDate = movieVM.ReleaseDate,
                    Genre = movieVM.Genre,
                    Price = movieVM.Price
                });
                UnitOfWork.Complete();
                return RedirectToAction("Index", "Movie");
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        [HttpGet("Edit")]
        public IActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                    throw new NullReferenceException();
                MovieViewModel movieVM = new MovieViewModel();
                var movie = UnitOfWork.MovieRepository.Get(id.Value);
                if (movie == null)
                    throw new NullReferenceException();
                movieVM.Id = movie.Id;
                movieVM.Genre = movie.Genre;
                movieVM.Price = movie.Price;
                movieVM.ReleaseDate = movie.ReleaseDate;
                movieVM.Title = movie.Title;
                return View(movieVM);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [FromForm] MovieViewModel movieVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Edit", "Movie", new { ID = id });
                }
                var movieM = UnitOfWork.MovieRepository.Get(id);
                movieM.Id = id;
                movieM.Genre = movieVM.Genre;
                movieM.Price = movieVM.Price;
                movieM.ReleaseDate = movieVM.ReleaseDate;
                movieM.Title = movieVM.Title;
                UnitOfWork.MovieRepository.Update(movieM);
                UnitOfWork.Complete();
                return RedirectToAction("Index", "Movie");
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        [HttpGet("Details")]
        public IActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                    throw new NullReferenceException();
                MovieViewModel movieVM = new MovieViewModel();
                var movie = UnitOfWork.MovieRepository.Get(id.Value);
                if (movie == null)
                    return NotFound();
                movieVM.Id = movie.Id;
                movieVM.Genre = movie.Genre;
                movieVM.Price = movie.Price;
                movieVM.ReleaseDate = movie.ReleaseDate;
                movieVM.Title = movie.Title;
                return View(movieVM);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }
        
        [HttpGet("Delete")]
        public IActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                    throw new NullReferenceException();
                MovieViewModel movieVM = new MovieViewModel();
                var movieM = UnitOfWork.MovieRepository.Get(id.Value);
                if (movieM == null)
                    throw new NullReferenceException();
                movieVM.Id = movieM.Id;
                movieVM.Genre = movieM.Genre;
                movieVM.Price = movieM.Price;
                movieVM.ReleaseDate = movieM.ReleaseDate;
                movieVM.Title = movieM.Title;
                return View(movieVM);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }            
        }

        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed([FromForm] int? id)
        {
            var movieM = UnitOfWork.MovieRepository.Get(id.Value);
            UnitOfWork.MovieRepository.Remove(movieM);
            UnitOfWork.Complete();
            return RedirectToAction("Index", "Movie");
        }
    }
}
