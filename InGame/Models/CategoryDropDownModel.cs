using InGame.Api.Helpers;
using InGame.Api.Models;
using InGame.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InGame.Web.Models
{
    public class CategoryDropDownModel
    {
        public long CategoryId { get; set; }        
        public IEnumerable<SelectListItem> CategoryList { get; set; }

        public CategoryDropDownModel()
        {
            var categories = RestHelper.GetObjects<CategoryResponseModel>(new Uri(Constants.Uri, "/api/category")).Result;
            CategoryList = new SelectList(categories, "Id", "Name");            
        }
    }
}
