﻿namespace BlazingBlog.Application.Articles.DeleteArticle
{
    public class DeleteArticleCommand : ICommand
    {
        public required int Id { get; set; }
    }
}
