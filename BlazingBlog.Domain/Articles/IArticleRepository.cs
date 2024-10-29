namespace BlazingBlog.Domain.Articles
{
	public interface IArticleRepository
	{
		Task<List<Article>> GetAllArticlesAsync();
		Task<Article?> GetArticleByIdAsync(int id);
		Task<List<Article>> GetArticlesByUserAsync(string userId);
		Task<Article> CreateArticleAsync(Article article);
		Task<Article?> UpdateArticleAsync(Article article);
		Task<bool> DeleteArticleAsync(int id);
	}
}
