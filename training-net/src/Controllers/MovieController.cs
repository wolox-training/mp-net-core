using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using training_net.Models.Views;
using training_net.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using training_net.Mail;
using training_net.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace training_net.Controllers
{
    [Authorize]
    [Route("Movie")]
    public class MovieController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public MovieController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
        }
        private IUnitOfWork UnitOfWork
        {
            get => this._unitOfWork;
        }

        private UserManager<User> UserManager
        {
            get => this._userManager;
        }

        [HttpGet("")]
        public IActionResult Index(string movieGenre, string searchString, string sortOrder, int? page, int? pageSize)
        {
            if(!pageSize.HasValue)
                pageSize = 10;            
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["GenreSortParm"] = sortOrder == "genre" ? "genre_desc" : "genre";
            ViewData["DateSortParm"] = sortOrder == "date" ? "date_desc" : "date";  
            ViewData["PriceSortParm"] = sortOrder == "price" ? "price_desc" : "price";
            ViewData["RatingSortParm"] = sortOrder == "rating" ? "rating_desc" : "rating";
            var movies = UnitOfWork.MovieRepository.GetAll();
            var genreQuery = movies.OrderBy(m => m.Genre).Select(m => m.Genre).Distinct().ToList();
            if (!String.IsNullOrEmpty(searchString))
                movies = movies.Where(m => m.Title.ToLower().Contains(searchString.ToLower()));
            if(!String.IsNullOrEmpty(movieGenre))
                movies = movies.Where(m => m.Genre == movieGenre);
            switch (sortOrder)
                {
                    case "rating_desc":
                        movies = movies.OrderByDescending(m => m.Rating).ToList();
                        break;
                    case "rating":
                        movies =movies.OrderBy(m => m.Rating).ToList();
                        break;
                    case "price_desc":
                        movies =movies.OrderByDescending(m => m.Price).ToList();
                        break;
                    case "price":
                        movies =movies.OrderBy(m => m.Price).ToList();
                        break;
                    case "date_desc":
                        movies =movies.OrderByDescending(m => m.ReleaseDate).ToList();
                        break;
                    case "date":
                        movies =movies.OrderBy(m => m.ReleaseDate).ToList();
                        break;
                    case "title_desc":
                        movies =movies.OrderByDescending(m => m.Title).ToList();
                        break;
                    case "genre":
                        movies =movies.OrderBy(m => m.Genre).ToList();
                        break;
                    case "genre_desc":
                        movies =movies.OrderByDescending(m => m.Genre).ToList();
                        break;
                    default:
                        movies =movies.OrderBy(m => m.Title).ToList();
                        break;
                }
            var moviesAndGenresVM = new MoviesAndGenresViewModel();
            moviesAndGenresVM.CurrentSearch = searchString;
            moviesAndGenresVM.CurrentGenre = movieGenre;
            moviesAndGenresVM.CurrentSort = sortOrder;
            moviesAndGenresVM.CurrentPageSize = pageSize.Value;
            moviesAndGenresVM.GenreList = new SelectList(genreQuery);
            var movieList = movies.Select( 
            movie => new MovieViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                Genre = movie.Genre,
                Price = movie.Price,
                Rating = movie.Rating
            }).ToList();
            moviesAndGenresVM.MovieList = PaginatedList<MovieViewModel>.Create(movieList, page ?? 1, pageSize.Value);    
            return View(moviesAndGenresVM);
        }

        [HttpGet("Create")]
        public IActionResult Create() => View();

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] MovieViewModel movieVM)
        {
            try
            {
                ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    UnitOfWork.MovieRepository.Add(new Movie
                    {
                        Title = movieVM.Title,
                        ReleaseDate = movieVM.ReleaseDate,
                        Genre = movieVM.Genre,
                        Price = movieVM.Price,
                        Rating = movieVM.Rating
                    });
                    UnitOfWork.Complete();
                    return RedirectToAction("Index", "Movie");
                }
                return View(movieVM);
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
                movieVM.Rating = movie.Rating;
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
                if(ModelState.IsValid)
                {
                    var movieM = UnitOfWork.MovieRepository.Get(id);
                    movieM.Id = id;
                    movieM.Genre = movieVM.Genre;
                    movieM.Price = movieVM.Price;
                    movieM.ReleaseDate = movieVM.ReleaseDate;
                    movieM.Title = movieVM.Title;
                    movieM.Rating = movieVM.Rating;
                    UnitOfWork.MovieRepository.Update(movieM);
                    UnitOfWork.Complete();
                    return RedirectToAction("Index", "Movie");
                }
                return View(movieVM);
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
                var movie = UnitOfWork.MovieRepository.GetMovieWithComments(id.Value);
                if (movie == null)
                    return NotFound();
                movieVM.Id = movie.Id;
                movieVM.Genre = movie.Genre;
                movieVM.Price = movie.Price;
                movieVM.ReleaseDate = movie.ReleaseDate;
                movieVM.Title = movie.Title;
                movieVM.Rating = movie.Rating;
                movieVM.Comments = movie.Comments;
                return View(movieVM);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpPost("Details")]
        public IActionResult AddComment(int? id,[FromForm] string comment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UnitOfWork.CommentRepository.Add(new Comment
                    {
                        User = UserManager.GetUserAsync(User).Result,
                        Text = comment,
                        Movie = UnitOfWork.MovieRepository.Get(id.Value) 
                    });
                    UnitOfWork.Complete();
                    return RedirectToAction("Details", "Movie", new { id = id.Value});
                }
                return View(id.Value);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
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
                movieVM.Rating = movieM.Rating;
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

        [HttpGet("SendMovieInfo")]
        public IActionResult SendMovieInfo(int? id)
        {
            try
            {
                if (id == null)
                    throw new NullReferenceException();
                MovieViewModel movieVM = new MovieViewModel();
                var movie = UnitOfWork.MovieRepository.Get(id.Value);
                if (movie == null)
                    throw new NullReferenceException();
                return View(new MovieViewModel
                {
                    Id = movie.Id,
                    Genre = movie.Genre,
                    Price = movie.Price,
                    ReleaseDate = movie.ReleaseDate,
                    Title = movie.Title,
                    Rating = movie.Rating
                });
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        [HttpPost("SendMovieInfo")]
        public IActionResult SendMovieInfo([FromForm] string EmailAddress, int? id)
        {
            try
            {
                if (id == null)
                    throw new NullReferenceException();
                var movie = UnitOfWork.MovieRepository.Get(id.Value);
                if (movie == null)
                    throw new NullReferenceException();
                string bodyMsg =    $@"Title: {movie.Title}{Environment.NewLine}
                                    Genre: {movie.Genre}{Environment.NewLine}
                                    Release date: {movie.ReleaseDate.ToShortDateString()}{Environment.NewLine}
                                    Price: {movie.Price}{Environment.NewLine}
                                    Rating: {movie.Rating}{Environment.NewLine}";
                Mailer.Send(EmailAddress, $"[Movie Info] {movie.Title}", bodyMsg);
                return RedirectToAction("Index", "Movie");
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }
    }
}
