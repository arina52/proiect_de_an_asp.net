﻿using culinaryConnect.BusinessLogic.Services;
using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.BusinessLogic.Models;
using culinaryConnect.Domain.Entities.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using culinaryConnect.BusinessLogic.Interfaces;
using culinaryConnect.BusinessLogic;
namespace Culinary_connect_web.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeService _recipeService;
        public RecipeController()
        {
            var bl = new BusinessLogic();
            _recipeService = new RecipeServiceBL();
        }

        // GET: Recipe
        public ActionResult Index(int id)
        {
            var recipePage = _recipeService.GetRecipeById(id);
            recipePage.FavoriteCount = _recipeService.GetFavoriteCount(recipePage.Id);
            if (Session["UserID"] != null)
            {
                var userId = (int)Session["UserID"];
                recipePage.IsFavorite = _recipeService.IsFavorite(userId, recipePage.Id);
            }
            if (recipePage == null)
            {
                return HttpNotFound();
            }
            return View(recipePage);
        }
    }
}