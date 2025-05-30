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
    public class TimelogsController : ControllerBase
    {
        private readonly ILogger<CoursesController> _logger;
        private readonly IAppBLL _bll;
        private readonly TimelogMapper _mapper = new TimelogMapper();

        public TimelogsController(ILogger<CoursesController> logger, IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
        }

        /// <summary>
        /// Get all timelogs available
        /// </summary>
        /// <returns>List of timelogs</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.Timelog> ), 200 )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.Timelog>>> GetTimelogs()
        {
            var data = await _bll.TimelogService.AllAsync(User.GetUserId());
            var res = data.Select(t => _mapper.Map(t)).ToList();

            return res;
        }

        /// <summary>
        /// Get timelog by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>timelog</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.Timelog>> GetTimelog(Guid id)
        {
            var timelog = await _bll.TimelogService.FindAsync(id, User.GetUserId());

            if (timelog == null)
            {
                return NotFound();
            }

            return _mapper.Map(timelog)!;;
        }

        /// <summary>
        /// Update timelog by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="timelog"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTimelog(Guid id, App.DTO.v1.Timelog timelog)
        {
            if (id != timelog.Id)
            {
                return BadRequest();
            }

            await _bll.TimelogService.UpdateAsync(_mapper.Map(timelog)!, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new course
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.Timelog>> PostTimelog(App.DTO.v1.TimelogCreate timelog)
        {
            var bllEntity = _mapper.Map(timelog);
            _bll.TimelogService.Add(bllEntity, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetTimelog", new
            {
                id = bllEntity.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, timelog);
        }

        /// <summary>
        /// Delete timelog by id - global
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimelog(Guid id)
        {
            await _bll.TimelogService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
