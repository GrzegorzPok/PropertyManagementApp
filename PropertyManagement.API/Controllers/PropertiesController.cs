using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyManagement.API.Data;
using PropertyManagement.API.Dtos;

namespace PropertyManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyManagementRepository _repo;
        private readonly IMapper _mapper;

        public PropertiesController(IPropertyManagementRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetProperties()
        {
            var properties = await _repo.GetProperties();

            var proportiesToReturn = _mapper.Map<IEnumerable<PropertyForDetailedDto>>(properties);

            return Ok(proportiesToReturn);
        }

        [HttpGet("{userId}/my")]
        public async Task<IActionResult> GetMyProperties(int userId)
        {
            var properties = await _repo.GetProperties();

            properties = properties.Where(p => p.OwnerId == userId);

            var proportiesToReturn = _mapper.Map<IEnumerable<PropertyForDetailedDto>>(properties);

            return Ok(proportiesToReturn);
        }
        
        [HttpGet("{userId}/rented")]
        public async Task<IActionResult> GetRentedProperties(int userId)
        {
            var rentedIds = await _repo.GetUserRents(userId);

            var properties = await _repo.GetProperties();

            properties = properties.Where(p => rentedIds.Any(r => r == p.Id));

            var proportiesToReturn = _mapper.Map<IEnumerable<PropertyForDetailedDto>>(properties);

            return Ok(proportiesToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProperty(int id)
        {
            var property = await _repo.GetProperty(id);

            var propertyToReturn = _mapper.Map<PropertyForDetailedDto>(property);
            
            return Ok(propertyToReturn);
        }

        [HttpGet("empty")]
        public async Task<IActionResult> GetEmptyProperty(int id)
        {
            var property = await _repo.GetEmptyProperty();

            var propertyToReturn = _mapper.Map<PropertyForDetailedDto>(property);
            
            return Ok(propertyToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty(int id, PropertyForDetailedDto propertyForDetailedDto)
        {
            if (id != 0)
            {
                var propertyFromRepo = await _repo.GetProperty(id);
                _mapper.Map(propertyForDetailedDto, propertyFromRepo);
             }
             else
             {
                 var propertyFromRepo = await _repo.GetEmptyProperty();
                _mapper.Map(propertyForDetailedDto, propertyFromRepo);
                _repo.Add(propertyFromRepo);
             }        

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception("Updating user {id} failed on save");
        }

        [HttpPost("{id}/rent/{userId}")]
        public async Task<IActionResult> RentProperty(int id, int userId)
        {
            var rent = await _repo.GetRent(userId, id);

            if (rent != null)
                return BadRequest("You already rent this property");

            if(await _repo.GetUser(userId) == null)
                return NotFound();

            rent = new Models.Rent
            {
                UserId = userId,
                PropertyId = id
            };

            _repo.Add<Models.Rent>(rent);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to rent property");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            var property = await _repo.GetProperty(id);
            if (property == null) {
                return NotFound();
            }

            _repo.Delete(property);
            
            if (await _repo.SaveAll())
                return Ok();

            throw new Exception("Error deleting the property");
        }
    }
}