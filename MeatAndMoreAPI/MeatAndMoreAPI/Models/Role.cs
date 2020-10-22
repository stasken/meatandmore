using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeatAndMoreAPI.Models
{
    public class Role : IdentityRole
    {
        [Required(ErrorMessage = "Description is required")]
        [StringLength(130, MinimumLength = 2, ErrorMessage = "Length must be between 2 and 130")]
        [RegularExpression(@"^[a-zA-Z0-9ÀàáÂâçÉéÈèÊêëïîÔô'-\.\s]+$", ErrorMessage = "Invalid characters used")]
        public string Description { get; set; }
        public ICollection<UserRoles> UserRoles { get; }
    }
}
