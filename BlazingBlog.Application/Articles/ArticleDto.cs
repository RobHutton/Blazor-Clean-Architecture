using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingBlog.Application.Articles
{
    public record struct ArticleDto (
        int Id,
        string Title,
        string? Content,
        DateTime DatePublished,
        bool IsPublished
    )
    {

    }
}
