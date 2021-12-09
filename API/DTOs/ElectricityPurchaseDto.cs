using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTOs
{
    public class ElectricityPurchaseDto
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string PurchasedBy { get; set; }
        public double Units { get; set; }
        public double Value { get; set; }
        public int PropertyId { get; set; }
        public PropertyDto Property { get; set; }
    }
}