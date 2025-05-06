using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using culinaryConnect.BusinessLogic.Models;
namespace culinaryConnect.BusinessLogic.Models
{
    public class CategoryDB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public List<string> Recipies { get; set; }
    }
}
