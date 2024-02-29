using RecipeDataAccess.DbContexts;
using RecipeDataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeDataAccess.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {

        public IRecipeUploadRepository RecipeUploadRepository { get; private set; }
        public IPublisherRepository publisherRepository { get; private set; }

        public IUserRecordsRepository userRecordsRepository { get; private set; }

        public IApplicationUserRepository applicationUserRepository { get; private set; }


        public IRecipeReviewRepository recipeReviewRepository { get; private set; }

        private readonly AppDbContext _db;
        public UnitOfWork(AppDbContext db)
        {
            _db = db;
           
            RecipeUploadRepository = new RecipeUploadRepository(_db);
            userRecordsRepository = new UserRecordsRepository(_db);
            applicationUserRepository = new ApplicationUserRepository(_db);
            recipeReviewRepository = new RecipeReviewRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
