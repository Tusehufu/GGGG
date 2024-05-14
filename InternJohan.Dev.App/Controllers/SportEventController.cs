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
        //H�mtar alla filmer
        // GET: api/Movies
        [HttpGet]

        public async Task<ActionResult<IEnumerable<SportEventViewModel>>> GetSportEvents()
        {
            var sportEvents = await _sportEventService.GetAll();
            return Ok(sportEvents);
        }

        //H�mtar en specifik film baserat p� id
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

        //    //L�gger till en ny film
        // POST: api/Movies
        [HttpPost]
        public async Task<ActionResult> PostSportEvent(SportEventViewModel sportEventViewModel)
        {
            // H�mta den inloggade anv�ndarens ID fr�n HttpContext
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Du m�ste vara inloggad f�r att skapa ett SportEvent.");
            }

            // Convert the user ID to an integer or handle conversion errors
            int userId;
            if (!int.TryParse(userIdClaim.Value, out userId))
            {
                return BadRequest("Ogiltigt anv�ndar-ID.");
            }

            // Skapa ett nytt SportEvent-objekt fr�n viewmodel
            SportEvent sportEvent = new SportEvent
            {
                Sport = sportEventViewModel.Sport,
                NeededParticipants = sportEventViewModel.NeededParticipants,
                Participants = sportEventViewModel.Participants,
                DateTime = sportEventViewModel.DateTime,
                Location = sportEventViewModel.Location,
                // S�tt UserHost till den inloggade anv�ndarens ID
                UserHost = new User { Id = userId }
            };

            // L�gg till sportEventet
            var id = await _sportEventService.Add(sportEvent);

            return Ok(id);
        }

        //    //Tar bort en film baserat p� id
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