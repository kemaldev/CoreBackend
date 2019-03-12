using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Services;

namespace CoreBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private IListRepository _listRepository;

        public ListController(IListRepository listRepository)
        {
            _listRepository = listRepository;

        }

        // GET: api/HuntedList
        //Get all of the lists
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var huntedLists = await _listRepository.AsyncGetAllHuntedLists();
            return Ok(huntedLists);
        }

        // GET: api/HuntedList/5
        //Get specific list by id
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var huntedList = _listRepository.GetHuntedList(id);
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

            await _listRepository.AsyncCreateHuntedList(huntedList);

            return Ok();
        }

        // PUT: api/HuntedList/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] HuntedList huntedList)
        {
            var list = await _listRepository.AsyncUpdateHuntedList(id, huntedList);

            if (list == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var huntedList = await _listRepository.AsyncDeleteHuntedList(id);

            if(huntedList == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
