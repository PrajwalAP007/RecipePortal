using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeDataAccess.Repository.Interface;
using RecipeModels.Models;
using System.Security.Claims;

namespace RecipePortal.Controllers
{
    [Authorize]
    public class RecipeReviewController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public RecipeReviewController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        private string GetUserId()
        {
           
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                return claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
         
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create(int id)
        {
            try
            {
                ViewBag.RecipeRatingId = id;
                return View();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }
        [HttpPost]
        public IActionResult Create(RecipeReview recipeReview )
        {
            try
            {

                recipeReview.RecipeiD = recipeReview.RecipeiD;
                recipeReview.ApplicationUserid = GetUserId();
                _unitOfWork.recipeReviewRepository.Add(recipeReview);
                _unitOfWork.Save();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }
    }
}
