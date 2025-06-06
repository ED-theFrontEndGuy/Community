using App.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using App.DTO.v1.Mappers;
using Asp.Versioning;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TripsController : ControllerBase
    {
        private readonly ILogger<TripsController> _logger;
        private readonly IAppBLL _bll;
        private readonly TripMapper _mapper = new TripMapper();

        public TripsController(ILogger<TripsController> logger, IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
        }

        /// <summary>
        /// Get all trips available
        /// </summary>
        /// <returns>List of trips</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.Trip> ), 200 )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.Trip>>> GetTrips()
        {
            var data = await _bll.TripService.AllAsync(User.GetUserId());
            var res = data.Select(c =>
            {
                var mapped = _mapper.Map(c)!;
                mapped.TripExpensesTotal = c.TripExpenses?.Sum(te => te.Expense?.ExpenseCost ?? 0) ?? 0;
                return mapped;
            }).ToList();

            return res;

        }

        /// <summary>
        /// Get trip by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>trip</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.Trip>> GetTrip(Guid id)
        {
            var trip = await _bll.TripService.FindAsync(id, User.GetUserId());

            if (trip == null)
            {
                return NotFound();
            }

            // return _mapper.Map(trip)!;
            var mapped = _mapper.Map(trip)!;
            mapped.TripExpensesTotal = trip.TripExpenses?.Sum(te => te.Expense?.ExpenseCost ?? 0) ?? 0;
            return mapped;
        }

        /// <summary>
        /// Update trip by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trip"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrip(Guid id, App.DTO.v1.Trip trip)
        {
            if (id != trip.Id)
            {
                return BadRequest();
            }

            await _bll.TripService.UpdateAsync(_mapper.Map(trip)!, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new trip
        /// </summary>
        /// <param name="trip"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.Trip>> PostTrip(App.DTO.v1.TripCreate trip)
        {
            var bllEntity = _mapper.Map(trip);
            _bll.TripService.Add(bllEntity, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetTrip", new
            {
                id = bllEntity.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, trip);
        }

        /// <summary>
        /// Delete trip by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip(Guid id)
        {
            await _bll.TripService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
