using System;
using System.Collections.Generic;
using System.Text;

namespace InGame.DAL.Models
{
    public class Category
    {
        public long Id { get; set; }
        public long ParentCategoryId { get; set; }
        public string Name { get; set; }
    }
}
