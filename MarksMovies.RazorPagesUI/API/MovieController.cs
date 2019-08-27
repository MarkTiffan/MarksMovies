using MarksMovies.Models;
using MarksMovies.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace MarksMovies.RazorPagesUI.API
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ExportService _expService;
        private readonly EditService _editService;
        private readonly DeleteService _deleteService;
        private readonly CreateService _createService;

        public MovieController(ExportService expService,
                                EditService editService,
                                DeleteService deleteService,
                                CreateService createService)
        {
            _expService = expService;
            _editService = editService;
            _deleteService = deleteService;
            _createService = createService;
        }

        /// <summary>
        /// Gets a list of Movie objects
        /// </summary>
        /// <returns>Serialized Json containing a collection of Movie objects</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IQueryable<Movie>> GetMovies()
        {
            var result = _expService.GetMovies();
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

        /// <summary>
        /// Gets a single Movie object
        /// </summary>
        /// <param name="ID">The ID of the Movie object to retrieve</param>
        /// <returns>Serialized Json of a single Movie object</returns>
        [HttpGet("{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Movie>> GetMovie(int ID)
        {
            var result = await _expService.GetMovie(ID);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }


        /// <summary>
        /// Edit a Movie for a given {ID}
        /// </summary>
        /// <param name="ID">The ID of the Movie object to edit</param>
        /// <param name="Movie">Json for the Movie object with edited values in the Request Body</param>
        /// <returns>Returns the updated Movie object</returns>
        [HttpPut("{ID}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Movie>> EditMovie(int ID, [FromBody] Movie Movie)
        {
            if (ID > 0 && Movie != null)
            {
                Movie.ID = ID;
                var result = await _editService.SaveMovieAsync(Movie);
                if (result > 0)
                    return Ok(Movie);
                else
                    return NotFound();
            }
            else
                return NotFound();
        }

        /// <summary>
        /// Create a new Movie from a json object
        /// </summary>
        /// <param name="Movie">Json for the Movie object to be created with values in the Request Body</param>
        /// <returns>Returns the new Movie ID value</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> CreateMovie([FromBody] Movie Movie)
        {
            if (Movie != null)
            {
                var result = await _createService.CreateAsync(Movie);
                if (result > 0)
                    return Ok(result); //result should be the ID of the new movie
                else
                    return BadRequest();
            }
            else
                return BadRequest();
        }


        /// <summary>
        /// Delete a Movie object for a given {ID}
        /// </summary>
        /// <param name="ID">ID of the Movie object to delete</param>
        /// <returns></returns>
        [HttpDelete("{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> DeleteMovie(int ID)
        {
            if (ID > 0)
            {
                var result = await _deleteService.DeleteMovieAsync(ID);
                if (result > 0)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return NotFound();
        }

    }
}