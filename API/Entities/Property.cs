using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    /// <summary>
    /// A Property represents an household owned by a user
    /// A Property can only be owned by one user
    /// </summary>
    [Table("Properties")]
    public class Property
    {
        public int Id { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
        
        // Address details
        public string Unit { get; set; }
        public string ComplexName { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public ICollection<ElectricityPurchase> ElectricityPurchases { get; set; }
    }
}