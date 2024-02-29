using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RecipeDataAccess.DbContexts;
using RecipeDataAccess.Repository.Interface;
using RecipeModels.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using static RecipeModels.Models.RevenueInsights;
namespace RecipePortal.Controllers
{
    public class RevenuInsightsController : Controller
    {
		private readonly IUnitOfWork _unitOfWork;
        public RevenuInsightsController(IUnitOfWork unitOfWork)
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
			var UserId = GetUserId();


			List<DataPoint> dataPoints = new List<DataPoint>();

			if (UserId != null)
			{
				var revenueData = (from ru in _unitOfWork.RecipeUploadRepository.GetAll()
								   join ur in _unitOfWork.userRecordsRepository.GetAll() on ru.RecipeId equals ur.RecipeiD
								   where _unitOfWork.RecipeUploadRepository.GetAll()
										 .Any(r => r.ApplicationUserid == UserId && r.RecipeId == ur.RecipeiD) && ur.PurchasePaymentStatus == "Success"
								   group ru by ru.Title into g
								   select new
								   {
									   Title = g.Key,
									   RecipeCount = g.Count() * 8
								   }).ToList();

				foreach (var data in revenueData)
				{
					dataPoints.Add(new DataPoint(data.Title, data.RecipeCount));
				}
			}
			if (dataPoints != null) {
				ViewBag.isRevenuGenerated = true;
			}
			else
			{
				ViewBag.isRevenuGenerated = false;
			}




			ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

			return View();
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
