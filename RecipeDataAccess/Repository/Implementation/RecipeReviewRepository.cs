using RecipeDataAccess.DbContexts;
using RecipeDataAccess.Repository.Interface;
using RecipeModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeDataAccess.Repository.Implementation
{
    public class RecipeReviewRepository : GenericRepository<RecipeReview>,IRecipeReviewRepository
    {
        private readonly AppDbContext _db;
        public RecipeReviewRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public void update(RecipeReview recipeReview)
        {
            _db.RecipeReviewsTbl.Update(recipeReview);
        }
    }
}