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
    }
}
