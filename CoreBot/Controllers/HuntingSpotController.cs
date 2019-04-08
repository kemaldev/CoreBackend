using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace CoreBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HuntingSpotController : ControllerBase
    {
        private IHuntingSpotRepository _repository;

        public HuntingSpotController(IHuntingSpotRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetHuntingSpotsAsync()
        {
            var huntingSpots = await _repository.GetAllHuntingSpotsAsync();

            return Ok(huntingSpots);
        }

        [HttpPost]
        public async Task<IActionResult> PostHuntingSpotAsync(HuntingSpot huntingSpot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddHuntingSpotAsync(huntingSpot);

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetHuntingSpot(int id)
        {
            var huntingSpot = _repository.GetHuntingSpot(id);

            if(huntingSpot == null)
            {
                return NotFound();
            }

            return Ok(huntingSpot);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHuntingSpot(int id)
        {
            var huntingSpot = await _repository.DeleteHuntingSpotAsync(id);
            if(huntingSpot == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditHuntingSpot(int id, HuntingSpot huntingSpot)
        {
            var hSpot = await _repository.UpdateHuntingSpotAsync(id, huntingSpot);
            if(hSpot == null)
            {
                return NotFound();
            }

            return Ok(hSpot);
        }
    }
}