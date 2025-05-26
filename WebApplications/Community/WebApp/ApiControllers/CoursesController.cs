using App.BLL.DTO;
using App.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
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
        private readonly IAppBLL _bll;

        public CoursesController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get all courses available
        /// </summary>
        /// <returns>List of courses</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.Course> ), 200 )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.Course>>> GetCourses()
        {
            var data = (await _bll.CourseService.AllAsync(User.GetUserId())).ToList();
            
            // ToDo - Add mapper
            var res = data.Select(c => new App.DTO.v1.Course
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            return res;
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseBLLDto>> GetCourse(Guid id)
        {
            var course = await _bll.CourseService.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(Guid id, CourseBLLDto course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            _bll.CourseService.Update(course);

            return NoContent();
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(CourseBLLDto course)
        {
            _bll.CourseService.Add(course, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new
            {
                // todo - get person id
                id = course.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var course = await _bll.CourseService.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _bll.CourseService.Remove(course);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
