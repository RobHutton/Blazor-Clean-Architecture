using BlazingBlog.Domain.Articles;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingBlog.Application.Articles.UpdateArticle
{
    public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, ArticleDto?>
    {
        private readonly IArticleRepository _articleRepository;
        public UpdateArticleCommandHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }
        public async Task<ArticleDto?> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var updateArticle = request.Adapt<Article>();
            var result = await _articleRepository.UpdateArticleAsync(updateArticle);
            if (result is null)
            {
                return null;
            }
            return result.Adapt<ArticleDto>();
        }
    }
}
