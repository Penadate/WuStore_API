using BLL.Services;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{gameId:int}")]
        public async Task<IActionResult> GetGameCategories(int gameId)
        {
            var categories = await _categoryService.GetCategoriesByGameIdAsync(gameId);
            return Ok(categories);
        }
    }
}
