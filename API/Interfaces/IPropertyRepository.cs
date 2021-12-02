using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetOwnedProperties(int id);
        Task<Property> GetProperty(int id);
        Task AddProperty(Property property);
        void UpdateProperty(Property property);
        void DeletePropertyAsync(Property property);
        void TransferProperty(Property property, AppUser receivingUser);
    }
}