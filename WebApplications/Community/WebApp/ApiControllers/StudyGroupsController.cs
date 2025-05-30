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
    public class StudyGroupsController : ControllerBase
    {
        private readonly ILogger<CoursesController> _logger;
        private readonly IAppBLL _bll;
        private readonly StudyGroupMapper _mapper = new StudyGroupMapper();

        public StudyGroupsController(ILogger<CoursesController> logger, IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
        }

        /// <summary>
        /// Get all study groups available
        /// </summary>
        /// <returns>List of study groups</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.Course> ), 200 )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.StudyGroup>>> GetStudyGroups()
        {
            var data = await _bll.StudyGroupService.AllAsync(User.GetUserId());
            var res = data.Select(s => _mapper.Map(s)).ToList();

            return res;
        }

        /// <summary>
        /// Get study group by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>study group</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.StudyGroup>> GetStudyGroup(Guid id)
        {
            var studyGroup = _bll.StudyGroupService.Find(id, User.GetUserId());

            if (studyGroup == null)
            {
                return NotFound();
            }

            return _mapper.Map(studyGroup)!;
        }

        /// <summary>
        /// Update study group by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studyGroup"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudyGroup(Guid id, App.DTO.v1.StudyGroup studyGroup)
        {
            if (id != studyGroup.Id)
            {
                return BadRequest();
            }

            await _bll.StudyGroupService.UpdateAsync(_mapper.Map(studyGroup)!, User.GetUserId());
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        /// <summary>
        /// Create new course
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.StudyGroup>> PostStudyGroup(App.DTO.v1.StudyGroupCreate studyGroup)
        {
            var bllEntity = _mapper.Map(studyGroup);
            _bll.StudyGroupService.Add(bllEntity, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetStudyGroup", new
            {
                id = bllEntity,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, studyGroup);
        }

        /// <summary>
        /// Delete study group by id - global
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudyGroup(Guid id)
        {
            await _bll.StudyGroupService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
