using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace CoreBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly HuntedListContext _context;

        public ListController(HuntedListContext context)
        {
            _context = context;
        }

        // GET: api/HuntedList
        //Get all of the lists
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var huntedLists = await _context.HuntedList.AsNoTracking().ToListAsync();
            return Ok(huntedLists);
        }

        // GET: api/HuntedList/5
        //Get specific list by id
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var huntedList = _context.HuntedList.Find(id);
            return Ok(huntedList);
        }

        // POST: api/HuntedList
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] HuntedList huntedList)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.HuntedList.Add(huntedList);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // PUT: api/HuntedList/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] HuntedList huntedList)
        {

            if(!_context.HuntedList.Any(t => t.Id == id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.HuntedList.Update(huntedList);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            var huntedList = _context.HuntedList.Find(id);

            if(huntedList == null)
            {
                return NotFound();
            }

            _context.HuntedList.Remove(huntedList);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
