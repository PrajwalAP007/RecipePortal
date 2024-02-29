using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace RecipeModels.Models
{
    public class RecipeUpload
    {
        [Key]
        public int RecipeId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string RecipeType { get; set; }
        [Required]
        public string RecipeLanguage { get; set; }
        [Required]

        public string RecipeDescription { get; set; }

        public string RecipeVideos { get; set; }

        public string RecipeMealType { get; set; }
        public string ? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile FormFile { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string? ApplicationUserid { get; set; }
        [ForeignKey("ApplicationUserid")]
        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }
        public string? PaymentStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }
    }
}
