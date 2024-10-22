namespace BlazingBlog.Application.Articles
{
    public record struct ArticleDto(
        int Id,
        string Title,
        string? Content,
        DateTime DatePublished,
        bool IsPublished
    )
    {

    }
}
