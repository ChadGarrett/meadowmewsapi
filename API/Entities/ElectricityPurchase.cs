using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("ElectricityPurchases")]
    public class ElectricityPurchase
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string PurchasedBy { get; set; }
        public double Units { get; set; }
        public double Value { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; }
    }
}