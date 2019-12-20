using MarksMovies.Models;
using MarksMovies.Services;
using MarksMovies.TMDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarksMovies.API
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly ExportService _expService;
        private readonly EditService _editService;
        private readonly DeleteService _deleteService;
        private readonly CreateService _createService;
        private readonly DetailsService _detailService;
        private readonly RankMoviesService _rankMoviesService;

        public MediaController(ExportService expService,
                                EditService editService,
                                DeleteService deleteService,
                                CreateService createService,
                                DetailsService detailService,
                                RankMoviesService rankMoviesService)
        {
            _expService = expService;
            _editService = editService;
            _deleteService = deleteService;
            _createService = createService;
            _detailService = detailService;
            _rankMoviesService = rankMoviesService;
        }

        /// <summary>
        /// Gets a list of media items
        /// </summary>
        /// <param name="Title">Partial title string to filter results by</param>
        /// <param name="Genre">Genre to filter results by</param>
        /// <returns>Serialized Json containing a collection of Media objects</returns>
        [HttpGet("items")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<Movie>>> GetMoviesAsync(string Title = "", GenreType Genre = 0)
        {
            var result = await _expService.GetMoviesAsync(Title, Genre);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

        /// <summary>
        /// Gets a single media item
        /// </summary>
        /// <param name="ID">The ID of the Media object to retrieve</param>
        /// <returns>Serialized Json of a single Media object</returns>
        [HttpGet("item/{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Movie>> GetMovieAsync(int ID)
        {
            var result = await _detailService.GetMovieAsync(ID);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }


        /// <summary>
        /// Edit a media item for a given {ID}
        /// </summary>
        /// <param name="ID">The ID of the Media object to edit</param>
        /// <param name="Movie">Json for the Media object with edited values in the Request Body</param>
        /// <returns>Returns the updated Media object</returns>
        [HttpPut("item/{ID}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Movie>> EditMovieAsync(int ID, [FromBody] Movie Movie)
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
        /// Create a new media item from a json object
        /// </summary>
        /// <param name="Movie">Json for the media object to be created with values in the Request Body</param>
        /// <returns>Returns the new media ID value</returns>
        [HttpPost("item")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> CreateMovieAsync([FromBody] Movie Movie)
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
        /// Delete a media item for a given {ID}
        /// </summary>
        /// <param name="ID">ID of the media item to delete</param>
        /// <returns></returns>
        [HttpDelete("item/{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> DeleteMovieAsync(int ID)
        {
            if (ID > 0)
            {
                var result = await _deleteService.DeleteMovieAsync(ID);
                if (result > 0)
                    return Ok(result);
                else
                    return BadRequest();
            }
            else
                return NotFound();
        }




       


        /// <summary>
        /// Retrieves a list of movies in ranked order
        /// </summary>
        /// <returns></returns>
        [HttpGet("rankedlist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<Movie>>> GetRankedMovies()
        {
            var result = _rankMoviesService.GetRankedMovies();
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

        /// <summary>
        /// Updates the ranking order for a list of movies
        /// </summary>
        /// <param name="itemIds">A comma separated list of item IDs in ranked order</param>
        /// <returns></returns>
        [HttpPost("rankedlist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> UpdateRanksAsync([FromBody] string itemIds)
        {
            if (string.IsNullOrEmpty(itemIds))
                return BadRequest();

            var result = await _rankMoviesService.UpdateRanksAsync(itemIds);
            if (result == 0)
                return NotFound();
            else
                return Ok(result);
        }


        /// <summary>
        /// Generates a JSON string with the full list of media items
        /// </summary>
        /// <returns></returns>
        [HttpGet("export/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> GetMoviesJsonAsync()
        {
            var result = await _expService.GetMoviesJsonAsync();
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }


        /// <summary>
        /// Generates a byte array for use as an Excel file export of the full list of media items
        /// </summary>
        /// <returns></returns>
        [HttpGet("export/excel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<byte[]>> GetExcelFileAsync()
        {
            var result = await _expService.GetExcelFileAsync();
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }
    }
}