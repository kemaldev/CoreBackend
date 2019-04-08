using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace CoreBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private ICharacterRepository _repository;

        public CharacterController(ICharacterRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetCharacters()
        {
            var characters = _repository.GetTibiaCharacters();
            return Ok(characters);
        }

        [HttpGet("{id}")]
        public IActionResult GetCharacter(int id)
        {
            var character = _repository.GetTibiaCharacter(id);
            if(character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }

        [HttpPost("{id}/huntingspot")]
        public async Task<IActionResult> AddHuntingSpotToCharacterAsync(int id, [FromQuery] int huntingSpotId)
        {
            var character = await _repository.AddHuntingSpotToCharacterAsync(id, huntingSpotId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (character == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{id}/huntingspot")]
        public async Task<IActionResult> DeleteHuntingSpotFromCharacterAsync(int id, [FromQuery] int huntingSpotId)
        {
            var character = await _repository.DeleteHuntingSpotFromCharacterAsync(id, huntingSpotId);

            if(character == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}