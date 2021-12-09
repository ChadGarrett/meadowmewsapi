using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public PropertyRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddProperty(Property property)
        {
            await _context.properties.AddAsync(property);
        }

        public void DeletePropertyAsync(Property property)
        {
            _context.properties.Remove(property);
        }

        public async Task<IEnumerable<PropertyDto>> GetOwnedProperties(int id)
        {
            return await _context.properties
                .Where(p => p.AppUserId == id)
                .ProjectTo<PropertyDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<Property> GetProperty(int id)
        {
            return await _context.properties
                .Include(u => u.AppUser)
                .SingleOrDefaultAsync(p => p.Id == id);
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