
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PropertyManagement.API.Data;

namespace PropertyManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            _context = context;
        }

        // GET api/values
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetValues()
        {
            return Ok(await _context.Values.ToListAsync());
        }
 
        // GET api/values/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetValue(int id)
        {
            return Ok(await _context.Values.FirstOrDefaultAsync(v => v.Id == id));
        }
 
        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
 
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
 
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
