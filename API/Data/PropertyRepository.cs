using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly DataContext _context;
        public PropertyRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddProperty(Property property)
        {
            await _context.properties.AddAsync(property);
        }

        public void DeletePropertyAsync(Property property)
        {
            _context.properties.Remove(property);
        }

        public async Task<IEnumerable<Property>> GetOwnedProperties(int id)
        {
            return await _context.properties
                .Where(p => p.AppUserId == id)
                .ToListAsync();
        }

        public async Task<Property> GetProperty(int id)
        {
            return await _context.properties.FindAsync(id);
        }

        public void TransferProperty(Property property, AppUser receivingUser)
        {
            throw new NotImplementedException();
        }

        public void UpdateProperty(Property property)
        {
            _context.Entry(property).State = EntityState.Modified;
        }
    }
}