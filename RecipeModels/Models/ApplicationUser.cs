﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace RecipeModels.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        public string Name { get; set; }


    }
}
