using RecipeModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RecipeDataAccess.Repository.Interface.IGenericRepository;

namespace RecipeDataAccess.Repository.Interface
{
    public interface IApplicationUserRepository : IGenericrepository<ApplicationUser>
    {
        void update(ApplicationUser applicationUser);   
    }
}
