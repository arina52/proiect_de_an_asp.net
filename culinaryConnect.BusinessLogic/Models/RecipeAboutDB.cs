using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace culinaryConnect.BusinessLogic.Models
{
    public class RecipeAboutDB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string CookingTime { get; set; }
        // list of ingredients or instructions will be separated by ###
        public string Ingredients { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
    }
}
