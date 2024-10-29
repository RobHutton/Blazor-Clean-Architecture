using BlazingBlog.Application.Articles;
using BlazingBlog.Application.Articles.GetArticles;
using BlazingBlog.Application.Articles.TogglePublishArticle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlazingBlog.WebUI.Server.Features.Articles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly string _err = "An unexpected error occurred on the server.";
        public ArticlesController(ISender sender)
        {
            _sender = sender;
        }
        [HttpGet]
        public async Task<ActionResult<List<ArticleDto>>> GetArticlesByCurrentUser()
        {
            var result = await _sender.Send(new GetArticlesByCurrentUserQuery());
            if (!result.Success)
            {
                return StatusCode(500, result.Error ?? _err);

            }
            return Ok(result.Value);
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult<ArticleDto>> TogglePublishArticle(int id)
        {
            var result = await _sender.Send(new TogglePublishArticleCommand { ArticleId = id });
            if (!result.Success)
            {
                return StatusCode(500, result.Error ?? _err);

            }
            return Ok(result.Value);
        }
    }
}
