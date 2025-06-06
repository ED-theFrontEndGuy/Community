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
    public class DeclarationsController : ControllerBase
    {
        private readonly ILogger<CoursesController> _logger;
        private readonly IAppBLL _bll;
        private readonly DeclarationMapper _mapper = new DeclarationMapper();

        public DeclarationsController(ILogger<CoursesController> logger, IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
        }

        /// <summary>
        /// Get all declarations available
        /// </summary>
        /// <returns>List of declarations</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.Declaration> ), 200 )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.Declaration>>> GetDeclarations()
        {
            var data = await _bll.DeclarationService.AllAsync(User.GetUserId());
            var res = data.Select(c => _mapper.Map(c)).ToList();

            return res;
        }

        /// <summary>
        /// Get declaration by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>declaration</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.Declaration>> GetDeclaration(Guid id)
        {
            var declaration = await _bll.DeclarationService.FindAsync(id, User.GetUserId());

            if (declaration == null)
            {
                return NotFound();
            }

            return _mapper.Map(declaration)!;;
        }

        /// <summary>
        /// Update declaration by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="declaration"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeclaration(Guid id, App.DTO.v1.Declaration declaration)
        {
            if (id != declaration.Id)
            {
                return BadRequest();
            }

            await _bll.DeclarationService.UpdateAsync(_mapper.Map(declaration)!, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new declaration
        /// </summary>
        /// <param name="declaration"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.Declaration>> PostDeclaration(App.DTO.v1.DeclarationCreate declaration)
        {
            var bllEntity = _mapper.Map(declaration);
            _bll.DeclarationService.Add(bllEntity, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetDeclaration", new
            {
                id = bllEntity.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, declaration);
        }

        /// <summary>
        /// Delete declaration by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeclaration(Guid id)
        {
            await _bll.DeclarationService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
