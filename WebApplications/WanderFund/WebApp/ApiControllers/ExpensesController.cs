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
    public class ExpensesController : ControllerBase
    {
        private readonly ILogger<ExpensesController> _logger;
        private readonly IAppBLL _bll;
        private readonly ExpenseMapper _mapper = new ExpenseMapper();

        public ExpensesController(IAppBLL bll, ILogger<ExpensesController> logger)
        {
            _bll = bll;
            _logger = logger;
        }

        /// <summary>
        /// Get all expenses available
        /// </summary>
        /// <returns>List of expenses</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.Expense> ), 200 )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.Expense>>> GetExpenses()
        {
            var data = await _bll.ExpenseService.AllAsync(User.GetUserId());
            var res = data.Select(c => _mapper.Map(c)).ToList();

            return res;
        }

        /// <summary>
        /// Get Expense by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Expense</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.Expense>> GetExpense(Guid id)
        {
            var expense = await _bll.ExpenseService.FindAsync(id, User.GetUserId());

            if (expense == null)
            {
                return NotFound();
            }

            return _mapper.Map(expense)!;
        }

        /// <summary>
        /// Update Expense by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Expense"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(Guid id, App.DTO.v1.Expense expense)
        {
            if (id != expense.Id)
            {
                return BadRequest();
            }

            await _bll.ExpenseService.UpdateAsync(_mapper.Map(expense)!, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new Expense
        /// </summary>
        /// <param name="Expense"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.Expense>> PostExpense(App.DTO.v1.ExpenseCreate expense)
        {
            var bllEntity = _mapper.Map(expense);
            _bll.ExpenseService.Add(bllEntity, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetExpense", new
            {
                id = bllEntity.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, expense);
        }

        /// <summary>
        /// Delete Expense by id - global
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(Guid id)
        {
            await _bll.ExpenseService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }
    }
}
