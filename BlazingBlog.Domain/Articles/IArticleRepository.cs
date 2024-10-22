using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingBlog.Domain.Articles
{
    public interface IArticleRepository
    {
        Task<List<Article>> GetAllArticlesAsync();
        Task<Article?> GetArticleByIdAsync(int id);
        Task<Article> CreateArticleAsync(Article article);
        Task<Article?> UpdateArticleAsync(Article article);
        Task<bool> DeleteArticleAsync(int id);
    }
}
