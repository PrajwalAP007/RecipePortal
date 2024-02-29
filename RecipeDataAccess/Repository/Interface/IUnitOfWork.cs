using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RecipeModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeDataAccess.Repository.Interface
{
    public interface IUnitOfWork
    {

        IRecipeUploadRepository RecipeUploadRepository { get; }
        IPublisherRepository publisherRepository { get; }
        IUserRecordsRepository userRecordsRepository { get; }
        IApplicationUserRepository applicationUserRepository { get; }
        IRecipeReviewRepository recipeReviewRepository { get; }
        void Save();
    }
}
