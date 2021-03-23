using System;

namespace MeadowMewsApi.Models {
    public class WaterStatement {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public bool Paid { get; set; }
    }
}