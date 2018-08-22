using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using training_net.Models.Views;
using training_net.Repositories.Database;
using training_net.Repositories.Interfaces;

namespace training_net.Controllers
{
  public class MovieController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public MovieController(IUnitOfWork unitOfWork){
            this._unitOfWork = unitOfWork;
        }
        public IUnitOfWork UnitOfWork
        {
            get { return this._unitOfWork; }
        }

        public IActionResult Index()
        {
                  
            return View();
        }
    }
}
