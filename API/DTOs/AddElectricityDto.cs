using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class AddElectricityDto
    {
        public DateTime PurchaseDate { get; set; }
        public string PurchasedBy { get; set; }
        public double Units { get; set; }
        public double Value { get; set; }
        public int PropertyId { get; set; }
    }
}