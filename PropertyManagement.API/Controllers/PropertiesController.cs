using System;
using System.Collections.Generic;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProperty(int id)
        {
            var property = await _repo.GetProperty(id);

            var propertyToReturn = _mapper.Map<PropertyForDetailedDto>(property);
            
            return Ok(propertyToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty(int id, PropertyForDetailedDto propertyForDetailedDto)
        {
            var propertyFromRepo = await _repo.GetProperty(id);

            _mapper.Map(propertyForDetailedDto, propertyFromRepo);
            //_repo.Add(propertyFromRepo);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception("Updating user {id} failed on save");
        }
    }
}