using InGame.Api.Helpers;
using InGame.Api.Models;
using InGame.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InGame.Web.Models
{
    public class CategoryDropDownModel
    {
        public List<CategoryResponseModel> Categories { get; set; }

        public CategoryDropDownModel()
        {
            Categories = RestHelper.GetObjects<CategoryResponseModel>(new Uri(Constants.Uri, "/api/category")).Result;
        }
    }
}
