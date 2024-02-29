using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeDataAccess.Repository.Interface;
using RecipeModels.Models;
using RecipeUtility;
using Stripe.Checkout;
using System.Security.Claims;

namespace RecipePortal.Controllers
{
    [Authorize]
    public class RecipeUploadController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
  
        public RecipeUploadController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RecipeUpload recipeUpload)
        {
            try
            {



                if (recipeUpload != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    if (recipeUpload.FormFile != null)
                    {
                        if (recipeUpload.ImageFile != null)
                        {
                            string imageFileName = Guid.NewGuid().ToString() + Path.GetExtension(recipeUpload.ImageFile.FileName);
                            string imagePath = Path.Combine(wwwRootPath, "RecipeImages", imageFileName);
                            using (var fileStream = new FileStream(imagePath, FileMode.Create))
                            {
                                recipeUpload.ImageFile.CopyTo(fileStream);
                            }
                            recipeUpload.ImageUrl = @"\RecipeImages\" + imageFileName;
                        }

                        // Handle video file upload
                        if (recipeUpload.FormFile != null)
                        {
                            string videoFileName = Guid.NewGuid().ToString() + Path.GetExtension(recipeUpload.FormFile.FileName);
                            string videoPath = Path.Combine(wwwRootPath, "RecipeVideos", videoFileName);
                            using (var fileStream = new FileStream(videoPath, FileMode.Create))
                            {
                                recipeUpload.FormFile.CopyTo(fileStream);
                            }
                            recipeUpload.RecipeVideos = @"\RecipeVideos\" + videoFileName;
                        }

                    }
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var UserId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    recipeUpload.ApplicationUserid = UserId;

                    var domain = "https://localhost:7235/";
                    var options = new Stripe.Checkout.SessionCreateOptions
                    {
                        SuccessUrl = domain + $"RecipeUpload/PaymentSuccess",
                        LineItems = new List<Stripe.Checkout.SessionLineItemOptions>
                {
                    new Stripe.Checkout.SessionLineItemOptions
                    {
                                Price = "price_1OlmiySDdjKt4XffvLNh0B1r",
                                Quantity=1,


                    },
                },
                        Mode = "payment",
                    };
                    var service = new Stripe.Checkout.SessionService();
                    Session session = service.Create(options);
                    recipeUpload.PaymentDate = DateTime.Now;
                    recipeUpload.SessionId = session.Id;
                    TempData["SessionId"] = session.Id;
                    Response.Headers.Add("Location", session.Url);
                    _unitOfWork.RecipeUploadRepository.Add(recipeUpload);
                    _unitOfWork.Save();


                    return new StatusCodeResult(303);






                }
                return RedirectToAction("Index", "Home");
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
                var sessionId = TempData.Peek("SessionId") as string;
                var service = new SessionService();
                Session session = service.Get(sessionId);
                if (sessionId != null)
                {
                    _unitOfWork.RecipeUploadRepository.updatePaymentStatus(sessionId, session.PaymentIntentId);
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
        public IActionResult Edit(int? id)
        {
            try
            {
                var data = _unitOfWork.RecipeUploadRepository.GetById(u => u.RecipeId == id);
                return View(data);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }

        }

        [HttpPost]
        public IActionResult Edit(RecipeUpload recipeUpload)
        {
            try
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (recipeUpload.ImageFile != null)
                {
                    string imageFileName = Guid.NewGuid().ToString() + Path.GetExtension(recipeUpload.ImageFile.FileName);
                    string imagePath = Path.Combine(wwwRootPath, "RecipeImages", imageFileName);
                    using (var fileStream = new FileStream(imagePath, FileMode.Create))
                    {
                        recipeUpload.ImageFile.CopyTo(fileStream);
                    }
                    recipeUpload.ImageUrl = @"\RecipeImages\" + imageFileName;
                }

                // Handle video file upload
                if (recipeUpload.FormFile != null)
                {
                    string videoFileName = Guid.NewGuid().ToString() + Path.GetExtension(recipeUpload.FormFile.FileName);
                    string videoPath = Path.Combine(wwwRootPath, "RecipeVideos", videoFileName);
                    using (var fileStream = new FileStream(videoPath, FileMode.Create))
                    {
                        recipeUpload.FormFile.CopyTo(fileStream);
                    }
                    recipeUpload.RecipeVideos = @"\RecipeVideos\" + videoFileName;
                }

                _unitOfWork.RecipeUploadRepository.update(recipeUpload);
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
        public IActionResult Delete(int? id)
        {
            try {
                var data = _unitOfWork.RecipeUploadRepository.GetById(u => u.RecipeId == id);
                return View(data);
            }
        
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteRecipe(RecipeUpload recipeUpload) 
        {
            try
            {
                _unitOfWork.RecipeUploadRepository.Delete(recipeUpload);
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
