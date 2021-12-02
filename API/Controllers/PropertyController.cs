using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PropertyController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public PropertyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        ///////////////
        // Endpoints //
        ///////////////

        [HttpGet("owned/id")]
        public async Task<IEnumerable<Property>> GetOwnedProperties(int id)
        {
            var appUserId = User.GetUserId();

            return await _unitOfWork.propertyRepository.GetOwnedProperties(id);
        }

        [HttpGet("id")]
        public async Task<Property> GetProperty(int id)
        {
            return await _unitOfWork.propertyRepository.GetProperty(id);
        }

        [HttpPost]
        public async Task<ActionResult<Property>> CreateProperty(Property property)
        {
            await _unitOfWork.propertyRepository.AddProperty(property);

            if (await _unitOfWork.Complete()) {
                return CreatedAtRoute("GetProperty", new { property = property.Id }, property);
            }

            return BadRequest("Could not create a new property");
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteProperty(int id)
        {
            var property = await _unitOfWork.propertyRepository.GetProperty(id);

            if (property == null) return NotFound();

            _unitOfWork.propertyRepository.DeletePropertyAsync(property);

            if (await _unitOfWork.Complete()) return NoContent();

            return BadRequest("Could not delete the property.");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProperty(Property property)
        {
            _unitOfWork.propertyRepository.UpdateProperty(property);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Could not update the property.");
        }
    }
}