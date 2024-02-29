using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeModels.Models
{
    public class PaginationViewModel<T>
    {
        public IEnumerable<T> Records { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
