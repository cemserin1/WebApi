using System;
using System.Collections.Generic;
using System.Text;

namespace InGame.DAL.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
    }
}
