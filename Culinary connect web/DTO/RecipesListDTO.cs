using culinaryConnect.Domain.Entities.Recipe;
using culinaryConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Culinary_connect_web.DTO
{
	public class RecipesListDTO
	{
		public List<RecipeDetails> RecipeList { get; set; }
		public List<CategoryUser> Categories { get; set; }
    }
}