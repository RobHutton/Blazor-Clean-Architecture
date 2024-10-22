using BlazingBlog.Domain.Articles;
using Microsoft.EntityFrameworkCore;

namespace BlazingBlog.Infrastructure.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _context;
        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Article> CreateArticleAsync(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
            return article;
        }

        public async Task<bool> DeleteArticleAsync(int id)
        {
            var deleteArticle = await GetArticleByIdAsync(id);
            if (deleteArticle == null)
            {
                return false;
            }
            _context.Articles.Remove(deleteArticle);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Article>> GetAllArticlesAsync()
        {
            return await _context.Articles.OrderBy(e => e.Title).ToListAsync();
        }

        public async Task<Article?> GetArticleByIdAsync(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            return article;
        }

        public async Task<Article?> UpdateArticleAsync(Article article)
        {
            var updateArticle = await GetArticleByIdAsync(article.Id);
            if (updateArticle == null)
            {
                return null;
            }
            updateArticle.Title = article.Title;
            updateArticle.Content = article.Content;
            updateArticle.DatePublished = article.DatePublished;
            updateArticle.IsPublished = article.IsPublished;
            updateArticle.DateUpdated = DateTime.Now;
            await _context.SaveChangesAsync();
            return updateArticle;
        }
    }
}
