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
        public  IActionResult GetListsAsync()
        {
            var huntedLists =  _listRepository.GetAllHuntedLists();
            return Ok(huntedLists);
        }

        // GET: api/HuntedList/5
        //Get specific list by id
        [HttpGet("{id}", Name = "Get")]
        public IActionResult GetList(int id)
        {
            var huntedList = _listRepository.GetHuntedList(id);
            if(huntedList == null)
            {
                return NotFound();
            }
            return Ok(huntedList);
        }

        // POST: api/HuntedList
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromQuery] string name)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _listRepository.CreateHuntedListAsync(name);

            return Ok();
        }

        //POST: api/list/{id}/character
        [HttpPost("{id}/character")]
        public async Task<IActionResult> PostCharacterToListAsync([FromQuery] string charName, int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var huntedList = await _listRepository.AddCharacterToListAsync(charName, id);
            if(huntedList == null) {
                return BadRequest();
            }

            return Ok();
        }


        [HttpPost("{id}/guild")]
        public async Task<IActionResult> PostGuildToListAsync([FromQuery] string guildName, int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var huntedList = await _listRepository.AddGuildToListAsync(guildName, id);
            if(huntedList == null)
            {
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/HuntedList/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromQuery] string name)
        {
            var list = await _listRepository.UpdateHuntedListAsync(id, name);

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
            var huntedList = await _listRepository.DeleteHuntedListAsync(id);

            if(huntedList == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}/guild")]
        public async Task<IActionResult> DeleteGuildAsync(int id, [FromQuery] string guildName)
        {
            var huntedList = await _listRepository.DeleteGuildFromListAsync(guildName, id);

            if (huntedList == null)
            {
                return NotFound();
            }

            return NoContent();
        }


        [HttpDelete("{id}/character")]
        public async Task<IActionResult> DeleteCharacterFromListAsync(int id, [FromQuery] int charId)
        {
            var huntedList = await _listRepository.DeleteCharacterFromListAsync(charId, id);

            if(huntedList == null) 
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
