using System;

namespace MeadowMewsApi.Models {
    public class ElectricityStatement {
        public long Id { get; set; }
        public long HouseholdId { get; set; }
        public Household Household { get; set; }
        public DateTime date { get; set; }
        public double Amount { get; set; }
        public double Charges { get; set; }
        public double Kwh { get; set; }
    }
}