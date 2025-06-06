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
    public class TripExpensesController : ControllerBase
    {
        private readonly ILogger<TripExpensesController> _logger;
        private readonly IAppBLL _bll;
        private readonly TripExpenseMapper _mapper = new TripExpenseMapper();

        public TripExpensesController(IAppBLL bll, ILogger<TripExpensesController> logger)
        {
            _bll = bll;
            _logger = logger;
        }

        /// <summary>
        /// Get all TripExpenses available
        /// </summary>
        /// <returns>List of TripExpenses</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.TripExpense> ), 200 )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.TripExpense>>> GetTripExpenses()
        {
            var data = await _bll.TripExpenseService.AllAsync(User.GetUserId());
            var res = data.Select(c => _mapper.Map(c)).ToList();

            return res;
        }

        /// <summary>
        /// Get TripExpense by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>TripExpense</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.TripExpense>> GetTripExpense(Guid id)
        {
            var tripExpense = await _bll.TripExpenseService.FindAsync(id, User.GetUserId());

            if (tripExpense == null)
            {
                return NotFound();
            }

            return _mapper.Map(tripExpense)!;
        }

        /// <summary>
        /// Update TripExpense by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="TripExpense"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTripExpense(Guid id, App.DTO.v1.TripExpense tripExpense)
        {
            if (id != tripExpense.Id)
            {
                return BadRequest();
            }

            await _bll.TripExpenseService.UpdateAsync(_mapper.Map(tripExpense)!, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new TripExpense
        /// </summary>
        /// <param name="TripExpense"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.TripExpense>> PostTripExpense(App.DTO.v1.TripExpenseCreate tripExpense)
        {
            var bllEntity = _mapper.Map(tripExpense);
            _bll.TripExpenseService.Add(bllEntity, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetTripExpense", new
            {
                id = bllEntity.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, tripExpense);
        }

        /// <summary>
        /// Delete TripExpense by id - global
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTripExpense(Guid id)
        {
            await _bll.TripExpenseService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }
    }
}
