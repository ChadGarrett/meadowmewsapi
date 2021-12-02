using System;

namespace API.Entities
{
    public class ElectricityPurchase
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string PurchasedBy { get; set; }
        public double Units { get; set; }
        public double Value { get; set; }
    }
}