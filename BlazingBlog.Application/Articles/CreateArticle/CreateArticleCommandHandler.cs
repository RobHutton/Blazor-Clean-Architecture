using BlazingBlog.Domain.Articles;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingBlog.Application.Articles.CreateArticle
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, ArticleDto>
    {
        private readonly IArticleRepository _articleRepository;
        public CreateArticleCommandHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }
        public async Task<ArticleDto> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var newArticle = request.Adapt<Article>();
            var article = await _articleRepository.CreateArticleAsync(newArticle);
            return article.Adapt<ArticleDto>();
        }
    }
}
