using BlazingBlog.Domain.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingBlog.Application.Articles
{
    public class ArticleService : IArticleService
    {
        public List<Article> GetAllArticles()
        {
            return new List<Article>
            {
                new Article { Id = 1, Title = "Article One", Content = "This is article one." },
                new Article { Id = 2, Title = "Article Second", Content = "This is article two." }
            };
        }
    }
}
