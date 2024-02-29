
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
    public class UserRecords
    {

        [Key]
        public int UserRecordId { get; set; }
        public string? ApplicationUserid { get; set; }
        [ForeignKey("ApplicationUserid")]
        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? RecipeiD { get; set; }

        [ForeignKey("RecipeiD")]
        [ValidateNever]
        public RecipeUpload? recipeUpload { get; set; }
        public string? PurchasePaymentID { get; set; }
        public string? PurchaseSessionId { get; set; }
        public string? PurchasePaymentStatus { get; set; }

    }
}