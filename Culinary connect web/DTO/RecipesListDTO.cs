using culinaryConnect.BusinessLogic.Models;
using culinaryConnect.Domain.Entities.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Culinary_connect_web.DTO
{
	public class RecipesListDTO
	{
		public List<RecipeDetails> RecipeList { get; set; }
	}
}