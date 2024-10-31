using BlazorCleanArchitecture.Application.Articles;
using System.Net.Http.Json;

namespace BlazorCleanArchitecture.WebUI.Client.Features.Articles
{
    public class MyArticlesService : IArticlesViewService
    {
        private readonly HttpClient _http;
        public MyArticlesService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<ArticleDto>?> GetArticlesByCurrentUserAsync()
        {
            return await _http.GetFromJsonAsync<List<ArticleDto>>("/api/Articles");
        }

        public async Task<ArticleDto?> TogglePublishArticleAsync(int articleId)
        {
            var result = await _http.PatchAsync($"api/Articles/{articleId}", null);
            if (result is not null && result.Content is not null)
            {
                return await result.Content.ReadFromJsonAsync<ArticleDto>();
            }
            return null;
        }
    }
}
