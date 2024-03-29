﻿using MarksMovies.Models;
using MarksMovies.Services;
using MarksMovies.TMDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MarksMovies.API
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly CreateService _createService;
        private readonly DetailsService _detailService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CreateService"></param>
        /// <param name="DetailsService"></param>
        public MovieController(CreateService CreateService,
                                DetailsService DetailsService)
        {
            _createService = CreateService;
            _detailService = DetailsService;
        }

        /// <summary>
        /// Fetch a movie from TMDB using a Title string
        /// </summary>
        /// <param name="Title">The full or partial string of a title to search for</param>
        /// <returns>SearchMovie object with the results of the fetch</returns>
        [HttpGet("fetch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SearchMovies>> FetchMovieAsync(string Title)
        {
            if (!string.IsNullOrEmpty(Title))
            {
                var result = await _createService.FetchMovieAsync(Title);
                if (result == null)
                    return BadRequest();
                else
                    return Ok(result);
            }
            else
                return NotFound();
        }


        /// <summary>
        /// Import fetched movie data into a Movie object
        /// </summary>
        /// <param name="TMDB_ID">The TMDB ID for the fetched movie</param>
        /// <returns>Updated media item with imported values</returns>
        [HttpGet("import")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Movie>> ImportMovieAsync(int TMDB_ID)
        {
            var Movie = new Movie();

            if (TMDB_ID <= 0)
                return Ok(Movie);

            var result = await _createService.ImportMovieAsync(TMDB_ID);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }


        /// <summary>
        /// Fetches the detailed movie info
        /// </summary>
        /// <param name="TMDBapi_ID">The ID for the movie from the TMDB api</param>
        /// <returns>MovieDetails object results from TMDB</returns>
        [HttpGet("details/{TMDBapi_ID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MovieDetails>> GetMovieDetailsAsync(int TMDBapi_ID)
        {
            if (TMDBapi_ID <= 0)
                return BadRequest();

            var result = await _detailService.GetMovieDetailsAsync(TMDBapi_ID);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }
    }
}
