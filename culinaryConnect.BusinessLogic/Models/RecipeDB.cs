using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using culinaryConnect.Domain.Entities.Recipe;
namespace culinaryConnect.BusinessLogic.Models
{
    public class RecipeDB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public int AboutRecipeID { get; set; }

        [ForeignKey("AboutRecipeID")]
        public RecipeAboutDB AboutRecipe { get; set; }
        [Required]
        public int CategoryID { get; set; }

        [Required]
        public string ImagePath { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public int AuthorID { get; set; }
    }
}
