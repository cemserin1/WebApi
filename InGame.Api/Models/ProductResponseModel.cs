using InGame.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InGame.Api.Models
{
    public class ProductResponseModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }

        public static ProductResponseModel Deserialize(string json)
        {
            ProductResponseModel prodd = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductResponseModel>(json);
            return prodd;
        }
    }
}
