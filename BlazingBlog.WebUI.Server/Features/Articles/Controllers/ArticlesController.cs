using BlazorCleanArchitecture.Application.Articles;
using Microsoft.AspNetCore.Mvc;

namespace BlazorCleanArchitecture.WebUI.Server.Features.Articles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticlesViewService _articlesViewService;
        public ArticlesController(IArticlesViewService articlesViewService)
        {
            _articlesViewService = articlesViewService;
        }
        [HttpGet]
        public async Task<ActionResult<List<ArticleDto>>> GetArticlesByCurrentUser()
        {
            var result = await _articlesViewService.GetArticlesByCurrentUserAsync();
            if (result is null)
            {
                return StatusCode(500, "Failed to get user articles.");

            }
            return Ok(result);
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult<ArticleDto>> TogglePublishArticle(int id)
        {
            var result = await _articlesViewService.TogglePublishArticleAsync(id);
            if (result is null)
            {
                return StatusCode(500, "Failed to toggle article publication.");

            }
            return Ok(result);
        }
    }
}
