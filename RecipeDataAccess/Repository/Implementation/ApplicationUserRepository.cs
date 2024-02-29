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
    public class ApplicationUserRepository:GenericRepository<ApplicationUser>,IApplicationUserRepository
    {
        private readonly AppDbContext _db;
        public ApplicationUserRepository(AppDbContext db): base(db) 
        {
            _db = db;
            
        }

        public void update(ApplicationUser applicationUser)
        {
        _db.ApplicationUsers.Update(applicationUser);
        }
    }
}
