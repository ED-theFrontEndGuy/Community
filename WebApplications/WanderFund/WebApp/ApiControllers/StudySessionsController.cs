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
    public class StudySessionsController : ControllerBase
    {
        private readonly ILogger<CoursesController> _logger;
        private readonly IAppBLL _bll;
        private readonly StudySessionMapper _mapper = new StudySessionMapper();

        public StudySessionsController(ILogger<CoursesController> logger, IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
        }

        /// <summary>
        /// Get all study sessions available
        /// </summary>
        /// <returns>List of study sessions</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.StudySession> ), 200 )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.StudySession>>> GetStudySessions()
        {
            var data = await _bll.StudySessionService.AllAsync(User.GetUserId());
            var res = data.Select(s => _mapper.Map(s)).ToList();

            return res;
        }

        /// <summary>
        /// Get study session by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>study session</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.StudySession>> GetStudySession(Guid id)
        {
            var studySession = await _bll.StudySessionService.FindAsync(id, User.GetUserId());

            if (studySession == null)
            {
                return NotFound();
            }

            return _mapper.Map(studySession)!;
        }

        /// <summary>
        /// Update study session by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studySession"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudySession(Guid id, App.DTO.v1.StudySession studySession)
        {
            if (id != studySession.Id)
            {
                return BadRequest();
            }

            await _bll.StudySessionService.UpdateAsync(_mapper.Map(studySession)!, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new study session
        /// </summary>
        /// <param name="studySession"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.StudySession>> PostStudySession(App.DTO.v1.StudySessionCreate studySession)
        {
            var bllEntity = _mapper.Map(studySession);
            _bll.StudySessionService.Add(bllEntity, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetStudySession", new
            {
                id = bllEntity.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, studySession);
        }

        /// <summary>
        /// Delete study session by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudySession(Guid id)
        {
            await _bll.StudySessionService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
