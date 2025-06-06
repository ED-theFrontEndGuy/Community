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
    public class UserTripController : ControllerBase
    {
        private readonly ILogger<UserTripController> _logger;
        private readonly IAppBLL _bll;
        private readonly UserTripMapper _mapper = new UserTripMapper();

        public UserTripController(IAppBLL bll, ILogger<UserTripController> logger)
        {
            _bll = bll;
            _logger = logger;
        }

        /// <summary>
        /// Get all UserTrips available
        /// </summary>
        /// <returns>List of UserTrips</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.UserTrip> ), 200 )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.UserTrip>>> GetUserTrips()
        {
            var data = await _bll.UserTripService.AllAsync(User.GetUserId());
            var res = data.Select(c => _mapper.Map(c)).ToList();

            return res;
        }

        /// <summary>
        /// Get UserTrip by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>UserTrip</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.UserTrip>> GetUserTrip(Guid id)
        {
            var userTrip = await _bll.UserTripService.FindAsync(id, User.GetUserId());

            if (userTrip == null)
            {
                return NotFound();
            }

            return _mapper.Map(userTrip)!;
        }

        /// <summary>
        /// Update UserTrip by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userTrip"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserTrip(Guid id, App.DTO.v1.UserTrip userTrip)
        {
            if (id != userTrip.Id)
            {
                return BadRequest();
            }

            await _bll.UserTripService.UpdateAsync(_mapper.Map(userTrip)!, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new UserTrip
        /// </summary>
        /// <param name="userTrip"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.UserTrip>> PostUserTrip(App.DTO.v1.UserTripCreate userTrip)
        {
            var bllEntity = _mapper.Map(userTrip);
            _bll.UserTripService.Add(bllEntity, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetUserTrip", new
            {
                id = bllEntity.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, userTrip);
        }

        /// <summary>
        /// Delete UserTrip by id - global
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserTrip(Guid id)
        {
            await _bll.UserTripService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }
    }
}
