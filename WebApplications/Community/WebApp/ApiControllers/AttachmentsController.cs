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
    public class AttachmentsController : ControllerBase
    {
        private readonly ILogger<AttachmentsController> _logger;
        private readonly IAppBLL _bll;
        private readonly AttachmentMapper _mapper = new AttachmentMapper();

        public AttachmentsController(IAppBLL bll, ILogger<AttachmentsController> logger)
        {
            _bll = bll;
            _logger = logger;
        }

        /// <summary>
        /// Get all attachments available
        /// </summary>
        /// <returns>List of attachments</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.Attachment> ), 200 )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.Attachment>>> GetAttachments()
        {
            var data = await _bll.AttachmentService.AllAsync(User.GetUserId());
            var res = data.Select(a => _mapper.Map(a)).ToList();

            return res;
        }

        /// <summary>
        /// Get attachment by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>attachment</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.Attachment>> GetAttachment(Guid id)
        {
            var attachment = await _bll.AttachmentService.FindAsync(id, User.GetUserId());

            if (attachment == null)
            {
                return NotFound();
            }

            return _mapper.Map(attachment)!;;
        }

        /// <summary>
        /// Update attachment by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="attachment"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttachment(Guid id, App.DTO.v1.Attachment attachment)
        {
            if (id != attachment.Id)
            {
                return BadRequest();
            }

            await _bll.AttachmentService.UpdateAsync(_mapper.Map(attachment)!, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new attachment
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.Attachment>> PostAttachment(App.DTO.v1.Attachment attachment)
        {
            var bllEntity = _mapper.Map(attachment);
            _bll.AttachmentService.Add(bllEntity, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetAttachment", new
            {
                id = bllEntity.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, attachment);
        }

        /// <summary>
        /// Delete attachment by id - owned by current user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttachment(Guid id)
        {
            await _bll.AttachmentService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
