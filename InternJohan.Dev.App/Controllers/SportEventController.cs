using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using InternJohan.Dev.Infrastructure.Models;
using InternJohan.Dev.Infrastructure.Repository;
using InternJohan.Dev.Infrastructure.ViewModel;
using System.Security.Claims;

namespace InternJohan.Dev.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportEventController : ControllerBase
    {
        private readonly SportEventService _sportEventService;
        public SportEventController(SportEventService sportEventService)
        {
            _sportEventService = sportEventService;
        }
        //Hämtar alla filmer
        // GET: api/Movies
        [HttpGet]

        public async Task<ActionResult<IEnumerable<SportEventViewModel>>> GetSportEvents()
        {
            var sportEvents = await _sportEventService.GetAll();
            return Ok(sportEvents);
        }

        //Hämtar en specifik film baserat på id
        // GET: api/Movies/5
        [HttpGet("{id}")]

        public async Task<ActionResult<SportEvent>> GetSportEvent(int id)
        {
            var sportEvent = await _sportEventService.FindById(id);

            if (sportEvent == null)
            {
                return NotFound("Sportevent not found");
            }

            return Ok(sportEvent);
        }

        //Uppdaterar en film
        // PUT: api/Movies/5
        [HttpPut("{id}")]
        [Authorize(Policy = "SportEventAccess")]
        public async Task<IActionResult> UpdateSportEvent(int id, SportEvent sportevent)
        {
            var oldSportEvent = await _sportEventService.FindById(id);
            if (oldSportEvent == null)
            {
                return NotFound("Sportevent not found");
            }
            sportevent.Id = oldSportEvent.Id;
            await _sportEventService.Update(sportevent);


            return NoContent();
        }

        //    //Lägger till en ny film
        // POST: api/Movies
        [HttpPost]
        public async Task<ActionResult> PostSportEvent(SportEventViewModel sportEventViewModel)
        {
            // Hämta den inloggade användarens ID från HttpContext
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Du måste vara inloggad för att skapa ett SportEvent.");
            }

            // Convert the user ID to an integer or handle conversion errors
            int userId;
            if (!int.TryParse(userIdClaim.Value, out userId))
            {
                return BadRequest("Ogiltigt användar-ID.");
            }

            // Skapa ett nytt SportEvent-objekt från viewmodel
            SportEvent sportEvent = new SportEvent
            {
                Sport = sportEventViewModel.Sport,
                NeededParticipants = sportEventViewModel.NeededParticipants,
                Participants = sportEventViewModel.Participants,
                DateTime = sportEventViewModel.DateTime,
                Location = sportEventViewModel.Location,
                // Sätt UserHost till den inloggade användarens ID
                UserHost = new User { Id = userId }
            };

            // Lägg till sportEventet
            var id = await _sportEventService.Add(sportEvent);

            return Ok(id);
        }

        //    //Tar bort en film baserat på id
        //    // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "SportEventAccess")]
        public async Task<IActionResult> DeleteSportEvent(int id)
        {
            if (await _sportEventService.Remove(id))
                return NoContent();
            return NotFound("Sportevent not found");
        }



    }
}