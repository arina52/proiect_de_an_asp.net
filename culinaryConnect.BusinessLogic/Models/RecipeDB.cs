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
        [StringLength(100)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string CategoryID { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        // can be both user and admin
        public string AuthorID { get; set; }
    }
}
