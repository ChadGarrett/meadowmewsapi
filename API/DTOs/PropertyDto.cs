using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTOs
{
    public class PropertyDto
    {
        public int Id { get; set; }
        public AppUserDto AppUserDto { get; set; }

        // Address details
        public string Unit { get; set; }
        public string ComplexName { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
    }
}