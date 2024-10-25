namespace BlazingBlog.Application.Articles.GetArticleForEdit
{
    public class GetArticleForEditQuery : IQuery<ArticleDto?>
    {
        public int Id { get; set; }
    }
}
