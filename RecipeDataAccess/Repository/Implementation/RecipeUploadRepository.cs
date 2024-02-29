using RecipeDataAccess.DbContexts;
using RecipeDataAccess.Repository.Interface;
using RecipeModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RecipeUtility;

namespace RecipeDataAccess.Repository.Implementation
{
    public class RecipeUploadRepository : GenericRepository<RecipeUpload>, IRecipeUploadRepository
    {
        private readonly AppDbContext _db;
        public RecipeUploadRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public void update(RecipeUpload recipeUpload)
        {
            _db.RecipeUploadTbl.Update(recipeUpload);
        }
        public void updatePaymentStatus( string sessionId, string paymentIntentId)
        {
            var orderFromDb = _db.RecipeUploadTbl.FirstOrDefault(u => u.SessionId == sessionId);
            if (sessionId != null)
            {
                orderFromDb.SessionId = sessionId;

            }
            if (paymentIntentId != null)
            {
                orderFromDb.PaymentIntentId = paymentIntentId;
                orderFromDb.PaymentDate = DateTime.Now;
                orderFromDb.PaymentStatus = RecipeConstant.SuccessPayment;

            }
            else
            {
                orderFromDb.PaymentDate = DateTime.Now;
                orderFromDb.PaymentStatus = RecipeConstant.FailPayment;
            }
        }
    }
}
