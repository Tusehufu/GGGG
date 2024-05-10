using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using InternJohan.Dev.Infrastructure.Models;
using InternJohan.Dev.Infrastructure.Repository;
using InternJohan.Dev.Infrastructure.ViewModel;

namespace InternJohan.Dev.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieService _movieService;
        public MovieController(MovieService movieService)
        {
            _movieService = movieService;
        }
        //Hämtar alla filmer
        // GET: api/Movies
        [HttpGet]

        public async Task<ActionResult<IEnumerable<MovieListViewModel>>> GetMovies()
        {
            var movies = await _movieService.GetAll();
            return Ok(movies);
        }

        //Hämtar en specifik film baserat på id
        // GET: api/Movies/5
        [HttpGet("{id}")]

        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _movieService.FindById(id);

            if (movie == null)
            {
                return NotFound("Movie not found");
            }

            return Ok(movie);
        }

        //Uppdaterar en film
        // PUT: api/Movies/5
        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateMovie(int id, Movie movie)
        {
            var oldMovie = await _movieService.FindById(id);
            if (oldMovie == null)
            {
                return NotFound("Movie not found");
            }
            movie.Id = oldMovie.Id;
            await _movieService.Update(movie);


            return NoContent();
        }

        //    //Lägger till en ny film
        // POST: api/Movies
        [HttpPost]

        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            var id = await _movieService.Add(movie);



            return Ok(id);
        }

        //    //Tar bort en film baserat på id
        //    // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (await _movieService.Remove(id))
                return NoContent();
            return NotFound("Movie not found");
        }

        //    //Kontrollerar om en film existerar
        //    private bool MovieExists(int id)
        //    {
        ///       return _context.Movies.Any(e => e.Id == id);
        //    }
        //}
    }
}