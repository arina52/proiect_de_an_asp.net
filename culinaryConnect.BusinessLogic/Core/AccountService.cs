using culinaryConnect.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using culinaryConnect.BusinessLogic.Models;
using culinaryConnect.BusinessLogic.Data;
using System.IO;
using culinaryConnect.Domain.Entities.Recipe;
using culinaryConnect.BusinessLogic.Models.UserDB;
using culinaryConnect.Domain.Entities.User;

namespace culinaryConnect.BusinessLogic.Core
{
    public class AccountService: IAcountService
    {
        private readonly CulinaryContext _context;

        public AccountService()
        {
            _context = new CulinaryContext();
        }
        public List<CategoryDB> GetCategoryList()
        {
            var categories = _context.Categories.ToList();
            return categories;
        }

        public User GetUserByID(int Id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == Id);
            return new User
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.UserEmail,
            };
        }
        public void AddRecipe(RecipesPageModel recipeModel, int userId, string image)
        {
            var recipe = recipeModel.RecipeForm;
            var aboutRecipe = new RecipeAboutDB
            {
                Description = recipe.AboutRecipe.Description,
                CookingTime = recipe.AboutRecipe.CookingTime,
                Ingredients = recipe.AboutRecipe.Ingredients,
                Instructions = recipe.AboutRecipe.Instructions
            };

            var newRecipe = new RecipeDB
            {
                Title = recipe.Title,
                AboutRecipe = aboutRecipe,
                CategoryID = recipe.CategoryID != 0 ? recipe.CategoryID : -1,
                CreatedDate = DateTime.Now,
                AuthorID = userId,
                ImagePath = image
            };


            _context.Recipes.Add(newRecipe);
            _context.SaveChanges();
        }

        public void DeleteRecipe(int id, int userId)
        {
            var recipe = _context.Recipes.Include("AboutRecipe").FirstOrDefault(r => r.Id == id);

            if (recipe == null || recipe.AuthorID != userId)
                throw new UnauthorizedAccessException("Not allowed to delete this recipe.");

            if (recipe.AboutRecipe != null)
                _context.RecipesAboutDb.Remove(recipe.AboutRecipe);

            _context.Recipes.Remove(recipe);
            _context.SaveChanges();
        }


        public void SubscribeUserToNewsletter(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserEmail == email);

            if (user == null)
            {
                var newUser = new UserDB
                {
                    UserEmail = email,
                    SubscribedToNews = true
                };

                _context.Users.Add(newUser);
            }
            else
            {
                user.SubscribedToNews = true;
            }

            _context.SaveChanges();
        }
        public void UpdateRecipe(RecipesPageModel model, int userId, string imageFileName = null)
        {
            var recipe = _context.Recipes.Include("AboutRecipe").FirstOrDefault(r => r.Id == model.RecipeForm.Id);

            if (recipe == null || recipe.AuthorID != userId)
                throw new UnauthorizedAccessException("Unauthorized access.");

            recipe.Title = model.RecipeForm.Title;
            recipe.CategoryID = model.RecipeForm.CategoryID;
            recipe.AboutRecipe.Description = model.RecipeForm.AboutRecipe.Description;
            recipe.AboutRecipe.Instructions = model.RecipeForm.AboutRecipe.Instructions;
            recipe.AboutRecipe.Ingredients = model.RecipeForm.AboutRecipe.Ingredients;
            recipe.AboutRecipe.CookingTime = model.RecipeForm.AboutRecipe.CookingTime;

            if (!string.IsNullOrEmpty(imageFileName))
            {
                recipe.ImagePath = imageFileName;
            }

            _context.SaveChanges();
        }


    }
}
