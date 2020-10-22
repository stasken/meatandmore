using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeatAndMoreAPI.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
        [Required]
        public string Username { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "<Pending>")]
        public ICollection<UserRolesDTO> Roles { get; set; }
        public string Token { get; set; }
    }
}
