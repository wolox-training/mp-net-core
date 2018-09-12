
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using training_net.Models;
using training_net.Models.Views;
using training_net.Repositories.Interfaces;

namespace training_net.Api.V1.Controllers
{
    [Route("api/v1/[controller]")]
    public class CommentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public CommentController(IUnitOfWork unitOfWork, UserManager<User> userManager)
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

        public IActionResult GetComments(int? id)
        {
            var movie = UnitOfWork.MovieRepository.GetMovieWithComments(id.Value);
            return Json(movie.Comments.ToList().Select(c => new CommentViewModel
            {
                Id = c.Id,
                Text = c.Text,
                UserName = c.User.UserName
            }));
        }

        public IActionResult AddComment(int? id,[FromForm] string comment)
        {
            try
            {
                UnitOfWork.CommentRepository.Add(new Comment
                {
                    User = UserManager.GetUserAsync(User).Result,
                    Text = comment,
                    Movie = UnitOfWork.MovieRepository.Get(id.Value) 
                });
                UnitOfWork.Complete();
                return GetComments(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}