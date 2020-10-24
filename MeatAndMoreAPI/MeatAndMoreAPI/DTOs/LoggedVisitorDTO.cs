using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeatAndMoreAPI.DTOs
{
    public class LoggedVisitorDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string TypeVisit { get; set; }
        public string CompanyName { get; set; }
        public string LicensePlate { get; set; }
        [Required]
        public DateTime LoggedIn { get; set; }
        public DateTime LoggedOut { get; set; }
        public bool InsideBuilding { get; set; }
    }
}
