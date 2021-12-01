using System;

namespace MeadowMewsApi.Models
{
    public class WaterStatement
    {
        public long Id { get; set; }
        public long HouseholdId { get; set; }
        public Household Household { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public bool Paid { get; set; }
    }

    public class WaterStatementDTO
    {
        public long Id { get; set; }
        public long HouseholdId { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public bool Paid { get; set; }
    }
}