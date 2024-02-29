using RecipeDataAccess.DbContexts;
using RecipeDataAccess.Repository.Interface;
using RecipeModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace RecipeDataAccess.Repository.Implementation
{
    public class PublisherRepository :GenericRepository<Publishers>,IPublisherRepository
    {
        private readonly AppDbContext _db;
        public PublisherRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

    
     
    }
}
