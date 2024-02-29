using RecipeDataAccess.DbContexts;
using RecipeDataAccess.Repository.Interface;
using RecipeModels.Models;
using RecipeUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace RecipeDataAccess.Repository.Implementation
{
    public class UserRecordsRepository : GenericRepository<UserRecords>, IUserRecordsRepository
    {
        private readonly AppDbContext _db;
        public UserRecordsRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void update(UserRecords userRecord)
        {
            _db.UserRecordsTbl.Update(userRecord);
        }
        public void updatePaymentStatus(string sessionId, string paymentIntentId)
        {
            var orderFromDb = _db.UserRecordsTbl.FirstOrDefault(u => u.PurchaseSessionId == sessionId);
            if (sessionId != null)
            {
                orderFromDb.PurchaseSessionId = sessionId;

            }
            if (paymentIntentId != null)
            {
                orderFromDb.PurchasePaymentID = paymentIntentId;

                orderFromDb.PurchasePaymentStatus = RecipeConstant.SuccessPayment;

            }
            else
            {

                orderFromDb.PurchasePaymentStatus = RecipeConstant.FailPayment;
            }
        }
    }
}
