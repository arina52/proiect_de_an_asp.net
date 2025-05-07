using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace culinaryConnect.BusinessLogic.Models.UserDB
{
    public class UserDB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        
        [StringLength(100)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string UserEmail { get; set; } = string.Empty;

        [StringLength(100)]
        public string PasswordHash { get; set; } = string.Empty;

        public bool SubscribedToNews { get; set; } = false;
    }
}
