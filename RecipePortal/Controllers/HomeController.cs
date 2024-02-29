using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeDataAccess.Repository.Interface;
using RecipeModels.Models;
using Stripe.Checkout;
using System.Diagnostics;
using System.Security.Claims;

namespace RecipePortal.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
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
            try
            {
                List<RecipeUpload> recipeUploads = _unitOfWork.RecipeUploadRepository.GetAll(includeProperties: "ApplicationUser").Where(u => u.PaymentStatus == "Success" ).ToList();
                var UserId = GetUserId();
                List<int> recipesId = _unitOfWork.RecipeUploadRepository.GetAll().Where(u => u.ApplicationUserid == UserId).Select(u => u.RecipeId).ToList();
                if (recipesId != null)
                {
                    ViewBag.RecipeId = recipesId.ToList();
                }
                else
                {
                    ViewBag.RecipeId = 0;
                }

                return View(recipeUploads);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }
       
        public IActionResult FilterByCategory(string? category, string? MealType, string? RecipeLanguage)
        {
            try
            {
                List<RecipeUpload> recipeUploads = _unitOfWork.RecipeUploadRepository
                    .GetAll(includeProperties: "ApplicationUser")
                    .Where(u => u.PaymentStatus == "Success" &&
                                (string.IsNullOrEmpty(category) || u.RecipeType == category) &&
                                (string.IsNullOrEmpty(MealType) || u.RecipeMealType == MealType) &&
                                (string.IsNullOrEmpty(RecipeLanguage) || u.RecipeLanguage == RecipeLanguage))
                    .ToList();
                var UserId = GetUserId();
                List<int> recipesId = _unitOfWork.RecipeUploadRepository.GetAll().Where(u => u.ApplicationUserid == UserId).Select(u => u.RecipeId).ToList();
                if (recipesId != null)
                {
                    ViewBag.RecipeId = recipesId.ToList();
                }
                else
                {
                    ViewBag.RecipeId = 0;
                }

                return View(recipeUploads);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }


        public IActionResult Summary(int id)
        {
            try
            {
                RecipeUpload recipeUpload = _unitOfWork.RecipeUploadRepository.GetById(u => u.RecipeId == id, includeProperties: "ApplicationUser");
                var reviewsForRecipe = _unitOfWork.recipeReviewRepository
            .GetAll()
            .Where(r => r.RecipeiD == recipeUpload.RecipeId);
                float averageRating = reviewsForRecipe.Any() ? ((float)reviewsForRecipe.Average(r => r.Rating)) : 0;
                ViewBag.AverageRating = Math.Round(averageRating, 1);
                return View(recipeUpload);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }

        }
        [HttpPost]
        public IActionResult Summary(RecipeUpload recipeUpload)
        {
            try
            {
                var UserId = GetUserId();
                ApplicationUser applicationUser = _unitOfWork.applicationUserRepository.GetById(u => u.Id == UserId);
                if (recipeUpload == null)
                {
                    // Handle case where recipeUpload is null
                    // For example, log an error or return an appropriate response
                    return BadRequest("RecipeUpload object is null.");
                }

                var domain = "https://localhost:7235/";
                var options = new Stripe.Checkout.SessionCreateOptions
                {
                    SuccessUrl = domain + $"Home/PaymentSuccess",
                    LineItems = new List<Stripe.Checkout.SessionLineItemOptions> {
                         new SessionLineItemOptions {
                            Price = "price_1Om8SoSDdjKt4XffzXB1vIDq",
                            TaxRates = new List<string> { "txr_1Om8RRSDdjKt4XffAsy9Bi60" },
                            Quantity=1,
                         },
                    },
                    Mode = "payment",
                };
                var service = new SessionService();
                Session session = service.Create(options);

                if (applicationUser != null)
                {
                    UserRecords records = new UserRecords()
                    {
                        Name = applicationUser.Name,
                        PhoneNumber = applicationUser.PhoneNumber,
                        Email = applicationUser.Email,
                        RecipeiD = recipeUpload.RecipeId,
                        PurchaseSessionId = session.Id,
                        ApplicationUserid = UserId
                    };
                    TempData["sessionId"] = session.Id;
                    Response.Headers.Add("Location", session.Url);
                    _unitOfWork.userRecordsRepository.Add(records);
                    _unitOfWork.Save();
                }
                else
                {
                    // Handle case where recipeUpload.ApplicationUser is null
                    // For example, log an error or return an appropriate response
                    return BadRequest("ApplicationUser object within RecipeUpload is null.");
                }

                return new StatusCodeResult(303);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }


        }



        public IActionResult PaymentSuccess()
        {
            try
            {
                var sessionId = TempData.Peek("sessionId") as string;
                var service = new SessionService();
                Session session = service.Get(sessionId);
                if (sessionId != null)
                {
                    _unitOfWork.userRecordsRepository.updatePaymentStatus(sessionId, session.PaymentIntentId);
                    _unitOfWork.Save();
                }
                return View();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
           
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult RecipesVideo()
        {
            try
            {
                var UserId = GetUserId();
                List<UserRecords> userRecords = _unitOfWork.userRecordsRepository.GetAll(includeProperties: "recipeUpload").Where(u => u.ApplicationUserid == UserId && u.PurchasePaymentStatus == "Success" && u.PurchasePaymentID != null).ToList();

                return View(userRecords);
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
