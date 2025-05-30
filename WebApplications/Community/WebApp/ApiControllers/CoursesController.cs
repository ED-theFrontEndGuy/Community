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
    public class CoursesController : ControllerBase
    {
        private readonly ILogger<CoursesController> _logger;
        private readonly IAppBLL _bll;
        // private readonly App.DTO.v1.Mappers.CourseMapper _mapper = new App.DTO.v1.Mappers.CourseMapper();
        private readonly CourseMapper _mapper = new CourseMapper();

        public CoursesController(IAppBLL bll, ILogger<CoursesController> logger)
        {
            _bll = bll;
            _logger = logger;
        }

        /// <summary>
        /// Get all courses availablef
        /// </summary>
        /// <returns>List of courses</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.Course> ), 200 )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.Course>>> GetCourses()
        {
            var data = await _bll.CourseService.AllAsync(User.GetUserId());
            var res = data.Select(c => _mapper.Map(c)).ToList();

            return res;
        }

        /// <summary>
        /// Get course by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.Course>> GetCourse(Guid id)
        {
            var course = await _bll.CourseService.FindAsync(id, User.GetUserId());

            if (course == null)
            {
                return NotFound();
            }

            return _mapper.Map(course)!;
        }

        /// <summary>
        /// Update course by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(Guid id, App.DTO.v1.Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            await _bll.CourseService.UpdateAsync(_mapper.Map(course)!, User.GetUserId());;
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new course
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.Course>> PostCourse(App.DTO.v1.CourseCreate course)
        {
            var bllEntity = _mapper.Map(course);
            _bll.CourseService.Add(bllEntity, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new
            {
                id = bllEntity.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, course);
        }

        /// <summary>
        /// Delete course by id - global
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            await _bll.CourseService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }
    }
}
