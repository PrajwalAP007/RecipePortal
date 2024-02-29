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
    public class Publishers
    {
        [Key]
        public int PublishersId { get; set; }
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
