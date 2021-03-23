using System;

namespace MeadowMewsApi.Models
{
    public class LevyStatement
    {
        public long Id { get; set; }
        public DateTime date { get; set; }
        public double Amount { get; set; }
        public string Notes { get; set; }
    }
}