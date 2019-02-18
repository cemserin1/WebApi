using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InGame.Web.Models
{
    public class CategoryModel
    {
        public long Id { get; set; }
        public long? ParentCategoryId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
