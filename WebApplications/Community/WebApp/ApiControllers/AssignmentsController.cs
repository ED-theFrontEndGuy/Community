using App.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
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
    public class AssignmentsController : ControllerBase
    {
        private readonly ILogger<CoursesController> _logger;
        private readonly IAppBLL _bll;
        private readonly AssignmentMapper _mapper = new AssignmentMapper();

        public AssignmentsController(ILogger<CoursesController> logger, IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
        }

        /// <summary>
        /// Get all assignments available
        /// </summary>
        /// <returns>List of assignments</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.Assignment> ), 200 )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.Assignment>>> GetAssignments()
        {
            var data = await _bll.AssignmentService.AllAsync(User.GetUserId());
            var res = data.Select(a => _mapper.Map(a)).ToList();
            
            return res;
        }

        /// <summary>
        /// Get assignment by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>course</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.Assignment>> GetAssignment(Guid id)
        {
            var assignment = await _bll.AssignmentService.FindAsync(id, User.GetUserId());

            if (assignment == null)
            {
                return NotFound();
            }

            return _mapper.Map(assignment)!;;
        }

        /// <summary>
        /// Update assignment by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="assignment"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssignment(Guid id, App.DTO.v1.Assignment assignment)
        {
            if (id != assignment.Id)
            {
                return BadRequest();
            }

            await _bll.AssignmentService.UpdateAsync(_mapper.Map(assignment)!, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new assignment
        /// </summary>
        /// <param name="assignment"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.Assignment>> PostAssignment(App.DTO.v1.Assignment assignment)
        {
            var bllEntity = _mapper.Map(assignment);
            _bll.AssignmentService.Add(bllEntity, User.GetUserId());
            await _bll.SaveChangesAsync();
            
            return CreatedAtAction("GetAssignment", new
            {
                id = bllEntity.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, assignment);
        }

        /// <summary>
        /// Delete assignment by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignment(Guid id)
        {
            await _bll.AssignmentService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
