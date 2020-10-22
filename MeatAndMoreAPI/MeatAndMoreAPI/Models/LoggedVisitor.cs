using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeatAndMoreAPI.Models
{
    public class LoggedVisitor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TypeVisit { get; set; }
        public string CompanyName { get; set; }
        public string LicensePlate { get; set; }
        public DateTime LoggedIn { get; set; }
        public DateTime LoggedOut { get; set; }
        public bool InsideBuilding { get; set; }
    }
}
