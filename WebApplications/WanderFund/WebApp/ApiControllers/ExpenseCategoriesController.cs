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
    public class ExpenseCategoriesController : ControllerBase
    {
        private readonly ILogger<ExpenseCategoriesController> _logger;
        private readonly IAppBLL _bll;
        private readonly ExpenseCategoryMapper _mapper = new ExpenseCategoryMapper();

        public ExpenseCategoriesController(IAppBLL bll, ILogger<ExpenseCategoriesController> logger)
        {
            _bll = bll;
            _logger = logger;
        }

        /// <summary>
        /// Get all expense categories available
        /// </summary>
        /// <returns>List of categories</returns>
        [HttpGet]
        [Produces( "application/json" )]
        [ProducesResponseType( typeof( IEnumerable<App.DTO.v1.ExpenseCategory> ), 200 )]
        [ProducesResponseType( 404 )]
        public async Task<ActionResult<IEnumerable<App.DTO.v1.ExpenseCategory>>> GetExpenseCategories()
        {
            var data = await _bll.ExpenseCategoryService.AllAsync(User.GetUserId());
            var res = data.Select(c => _mapper.Map(c)).ToList();

            return res;
        }

        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>category</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<App.DTO.v1.ExpenseCategory>> GetExpenseCategory(Guid id)
        {
            var expenseCategory = await _bll.ExpenseCategoryService.FindAsync(id, User.GetUserId());

            if (expenseCategory == null)
            {
                return NotFound();
            }

            return _mapper.Map(expenseCategory)!;
        }

        /// <summary>
        /// Update category by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpenseCategory(Guid id, App.DTO.v1.ExpenseCategory expenseCategory)
        {
            if (id != expenseCategory.Id)
            {
                return BadRequest();
            }

            await _bll.ExpenseCategoryService.UpdateAsync(_mapper.Map(expenseCategory)!, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create new category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<App.DTO.v1.ExpenseCategory>> PostExpenseCategory(App.DTO.v1.ExpenseCategory expenseCategory)
        {
            var bllEntity = _mapper.Map(expenseCategory);
            _bll.ExpenseCategoryService.Add(bllEntity, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetExpenseCategory", new
            {
                id = bllEntity.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, expenseCategory);
        }

        /// <summary>
        /// Delete category by id - global
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpenseCategory(Guid id)
        {
            await _bll.ExpenseCategoryService.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }
    }
}
