using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using culinaryConnect.BusinessLogic.Data;
using culinaryConnect.BusinessLogic.Models;
using culinaryConnect.BusinessLogic.Models.UserDB;
using culinaryConnect.Domain.Entities;
using culinaryConnect.Domain.Entities.Recipe;
using culinaryConnect.Domain.Entities.User;

namespace culinaryConnect.BusinessLogic
{
    public class UserApi
    {
        private readonly CulinaryContext _context = new CulinaryContext();

        public UserDB GetUserByEmailandPasswordAction(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserEmail == email && u.PasswordHash == password);
            return user;
        }
        public UserDB GetUserByEmailAction(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserEmail == email);
            return user;
        }
        public void AddNewUserToDB(UserDB user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void UpdateSemiExistingUserDB(UserDB user, string hashedPassword, string userEmail)
        {
            user.PasswordHash = hashedPassword;
            user.UserName = userEmail;
            _context.SaveChanges();
        }
        public List<UserDB> GetAllUsersFromDB()
        {
            var users = _context.Users.ToList();
            return users;
        }
        public List<CategoryDB> GetCategoryListFromDB()
        {
            var categories = _context.Categories.ToList();
            return categories;
        }
        public User GetUserByIdFromDB(int Id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == Id);
            return new User
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.UserEmail,
            };
        }
        public void AddRecipeDB(RecipesPageModel recipeModel, int userId, string image)
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
        public void DeleteRecipeDB(int id, int userId)
        {
            var recipe = _context.Recipes.Include("AboutRecipe").FirstOrDefault(r => r.Id == id);

            if (recipe == null || recipe.AuthorID != userId)
                throw new UnauthorizedAccessException("Not allowed to delete this recipe.");

            if (recipe.AboutRecipe != null)
                _context.RecipesAboutDb.Remove(recipe.AboutRecipe);

            _context.Recipes.Remove(recipe);
            _context.SaveChanges();
        }
        public void SubscribeUserToNewsletterAction(string email)
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
        public void UpdateRecipeAction(RecipesPageModel model, int userId, string imageFileName = null)
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
        public List<RecipeDetails> GetAllRecipesFromDB()
        {
            var recipes = _context.Recipes.Include("AboutRecipe").ToList();
            var authorIds = recipes.Select(r => r.AuthorID).Distinct().ToList();
            var authors = _context.Users
                .Where(u => authorIds.Contains(u.Id))
                .Select(u => new AuthorModel { ID = u.Id, Name = u.UserName })
                .ToList();
            return recipes.Select(r => new RecipeDetails
            {
                Id = r.Id,
                Title = r.Title,
                CategoryID = r.CategoryID,
                ImagePath = r.ImagePath,
                CreatedDate = r.CreatedDate.ToShortDateString(),
                Author = authors.FirstOrDefault(a => a.ID == r.AuthorID),
                AboutRecipe = r.AboutRecipe != null ? new RecipeAbout
                {
                    Description = r.AboutRecipe.Description,
                    CookingTime = r.AboutRecipe.CookingTime,
                    Instructions = r.AboutRecipe.Instructions,
                    Ingredients = r.AboutRecipe.Ingredients
                } : null
            }).ToList();
        }
        public List<RecipeDetails> GetAllActiveRecipesFromDB()
        {
            var recipes = _context.Recipes.Include("AboutRecipe").Where(r => r.Status == Status.Active).ToList();
            var authorIds = recipes.Select(r => r.AuthorID).Distinct().ToList();
            var authors = _context.Users
                .Where(u => authorIds.Contains(u.Id))
                .Select(u => new AuthorModel { ID = u.Id, Name = u.UserName })
                .ToList();
            return recipes.Select(r => new RecipeDetails
            {
                Id = r.Id,
                Title = r.Title,
                CategoryID = r.CategoryID,
                ImagePath = r.ImagePath,
                CreatedDate = r.CreatedDate.ToShortDateString(),
                Author = authors.FirstOrDefault(a => a.ID == r.AuthorID),
                AboutRecipe = r.AboutRecipe != null ? new RecipeAbout
                {
                    Description = r.AboutRecipe.Description,
                    CookingTime = r.AboutRecipe.CookingTime,
                    Instructions = r.AboutRecipe.Instructions,
                    Ingredients = r.AboutRecipe.Ingredients
                } : null
            }).ToList();
        }
        public RecipeDetails GetRecipeByIdFromDB(int Id)
        {
            var recipeDB = _context.Recipes.Include("AboutRecipe").Include("FavoritedByUsers").FirstOrDefault(r => r.Id == Id);
            var author = _context.Users.FirstOrDefault(u => u.Id == recipeDB.AuthorID);

            if (recipeDB == null) return null;

            return new RecipeDetails
            {
                Id = recipeDB.Id,
                Title = recipeDB.Title,
                ImagePath = recipeDB.ImagePath,
                CategoryID = recipeDB.CategoryID,
                CreatedDate = recipeDB.CreatedDate.ToShortDateString(),
                Author = new AuthorModel
                {
                    ID = author.Id,
                    Name = author.UserName
                },
                AboutRecipe = recipeDB.AboutRecipe != null ? new RecipeAbout
                {
                    Description = recipeDB.AboutRecipe.Description,
                    CookingTime = recipeDB.AboutRecipe.CookingTime,
                    Instructions = recipeDB.AboutRecipe.Instructions,
                    Ingredients = recipeDB.AboutRecipe.Ingredients
                } : null
            };
        }
        public RecipeDB GetRecipeEntityByIdFromDB(int Id)
        {
            var recipe = _context.Recipes.Include("AboutRecipe").FirstOrDefault(r => r.Id == Id);
            return recipe;
        }
        public void AddRecipeToFavorites(int userId, int recipeId)
        {
            var exists = _context.FavoriteRecipes
            .Any(f => f.UserId == userId && f.RecipeId == recipeId);

            if (!exists)
            {
                _context.FavoriteRecipes.Add(new FavoriteRecipeDB
                {
                    UserId = userId,
                    RecipeId = recipeId
                });
                _context.SaveChanges();
            }
        }
        public void RemoveRecipeFromFavorites(int userId, int recipeId)
        {
            var favorite = _context.FavoriteRecipes
                .FirstOrDefault(f => f.UserId == userId && f.RecipeId == recipeId);

            if (favorite != null)
            {
                _context.FavoriteRecipes.Remove(favorite);
                _context.SaveChanges();
            }
        }
        public bool IsFavoriteRecipe(int userId, int recipeId)
        {
            return _context.FavoriteRecipes
                .Any(f => f.UserId == userId && f.RecipeId == recipeId);
        }
        public List<RecipeDetails> GetUserFavoritesList(int userId)
        {
            var favorites = _context.FavoriteRecipes
                .Where(f => f.UserId == userId)
                .Include(f => f.Recipe.AboutRecipe)
                .Select(f => new RecipeDetails
                {
                    Id = f.Recipe.Id,
                    Title = f.Recipe.Title,
                    ImagePath = f.Recipe.ImagePath,
                    AboutRecipe = new RecipeAbout
                    {
                        Description = f.Recipe.AboutRecipe.Description,
                        Ingredients = f.Recipe.AboutRecipe.Ingredients,
                        Instructions = f.Recipe.AboutRecipe.Instructions,
                        CookingTime = f.Recipe.AboutRecipe.CookingTime
                    },
                    CategoryID = f.Recipe.CategoryID,
                    DateCreated = f.Recipe.CreatedDate,
                    Author = new AuthorModel
                    {
                        ID = f.Recipe.AuthorID,
                        Name = _context.Users.FirstOrDefault(u => u.Id == f.Recipe.AuthorID).UserName,
                    },
                    FavoriteCount = f.Recipe.FavoritedByUsers.Count(),
                    IsFavorite = true
                })
                .ToList();

            return favorites;
        }
        public int GetRecipeFavoriteCount(int recipeId)
        {
            return _context.FavoriteRecipes
                .Count(f => f.RecipeId == recipeId);
        }


    }
}
