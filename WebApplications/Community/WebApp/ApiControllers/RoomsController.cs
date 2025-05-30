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
    public class RoomsController : ControllerBase
    {
        private readonly ILogger<CoursesController> _logger;
        private readonly IAppBLL _bll;
        private readonly RoomMapper _mapper = new RoomMapper();

        public RoomsController(ILogger<CoursesController> logger, IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
        }

        /// <summary>
        /// Get all rooms available
        /// </summary>
        /// <returns>List of rooms</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.Room> ), 200 )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.Room>>> GetRooms()
        {
            var data = await _bll.RoomService.AllAsync(User.GetUserId());
            var res = data.Select(r => _mapper.Map(r)).ToList();

            return res;
        }

        /// <summary>
        /// Get room by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>room</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.Room>> GetRoom(Guid id)
        {
            var room = await _bll.RoomService.FindAsync(id, User.GetUserId());

            if (room == null)
            {
                return NotFound();
            }

            return _mapper.Map(room)!;
        }

        /// <summary>
        /// Update room by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="room"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(Guid id, App.DTO.v1.Room room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }

            await _bll.RoomService.UpdateAsync(_mapper.Map(room)!, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new room
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.Room>> PostRoom(App.DTO.v1.RoomCreate room)
        {
            var bllEntity = _mapper.Map(room);
            _bll.RoomService.Add(bllEntity, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetRoom", new
            {
                id = bllEntity.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, room);
        }

        /// <summary>
        /// Delete room by id - global
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(Guid id)
        {
            await _bll.RoomService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
