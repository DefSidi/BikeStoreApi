using Library.BikeApplication;
using Library.BikeApplication.Interface;
using Library.BikeApplication.Model;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace Web.BikeApplication.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BikeController : ControllerBase
    {
        private readonly IBikeService _bikeService;
        public BikeController(IBikeService bikeService)
        {
           // _bikeRepository = bikeRepository;
            _bikeService = bikeService;
            

        }
        /// <summary>
        /// Retrieves the list of bikes.
        /// </summary>
        /// <returns>An IActionResult representing the HTTP response with the list of bikes if successful;
        /// otherwise, a BadRequest or a Notfound response with the error message.</returns>
        // GET: api/<BikeController>
        [HttpGet]
        public IActionResult GetBikes()
        {
            try
            {
                var result = _bikeService.GetBikes();
                if (result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves a bike by its ID.
        /// </summary>
        /// <param name="id">The ID of the bike to retrieve.</param>
        /// <returns>An IActionResult representing the HTTP response with the bike if found;
        /// otherwise, a BadRequest a BadRequest or a Notfound response with the error message.</returns>
        // GET api/<BikeController>/5
        [HttpGet("{id}")]
        public IActionResult GetBikeById(int id)
        {
            try
            {
                var result = _bikeService.GetBikeById(id);
                if (result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Adds a new bike
        /// </summary>
        /// <param name="newBike">The new bike to be added.</param>
        /// <returns>An IActionResult representing the HTTP response with the bike if found;
        /// otherwise, a BadRequest a BadRequest or a Notfound response with the error message.</returns>
        // POST api/<BikeController>
        [HttpPost]
        public IActionResult AddBike([FromBody] Bike newBike)
        {
            try
            {
                var result = _bikeService.AddBike(newBike);
                if (result.Success)
                {
                    return Ok("Bike was Added successfully.");
                }
                else
                {
                    return NotFound(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates and existing bike.
        /// </summary>
        /// <param name="id">The id of the bike to be updated.</param>
        /// <param name="bike">The changed data of the bike.</param>
        /// <returns>An IActionResult representing the HTTP response with the bike if found;
        /// otherwise, a BadRequest a BadRequest or a Notfound response with the error message.</returns>
        // PUT api/<BikeController>/5
        [HttpPut("{id}")]
        public IActionResult EditBike(int id, [FromBody] Bike bike)
        {
            
                var result = _bikeService.EditBikeById(id, bike);
                if (result.Success)
                {
                    return Ok("Bike was Edited successfully.");
                }
                else
                {
                    return NotFound(result.ErrorMessage);
                }
        }

        /// <summary>
        /// Deletes a Bike by id.
        /// </summary>
        /// <param name="id">The id passed to delete the Bike from the JSON file.</param>
        /// <returns>An IActionResult representing the HTTP response with the bike if found;
        /// otherwise, a BadRequest a BadRequest or a Notfound response with the error message.</returns>
        // DELETE api/<BikeController>/DeleteBike/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBike(int id)
        {
            try
            {
                var result = _bikeService.DeleteBikeById(id);

                if (result.Success)
                {
                    return Ok("Bike was deleted successfully.");
                }
                else
                {
                    return NotFound(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
