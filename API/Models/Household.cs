using System;
using MeadowMewsApi.Models;

namespace MeadowMewsApi.Models
{
    public class Household
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
    }

    public class HouseholdDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
    }
}