using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeModels.Models
{
    public class RecipeReview
    {
        [Key]
        public int RecipeRatingId { get; set; }
        [Required]
        public string? ApplicationUserid { get; set; }
        [ForeignKey("ApplicationUserid")]
        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }
        [Required]
        public int? RecipeiD { get; set; }

        [ForeignKey("RecipeiD")]
        [ValidateNever]
        public RecipeUpload? recipeUpload { get; set; }
        public int Rating { get; set; }
        [Required]
        public string Review { get; set; }

    }
}
