using BlazingBlog.Application.Articles.GetArticleById;
using BlazingBlog.Domain.Articles;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingBlog.Application.Articles.GetArticlebyId
{
    public class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, ArticleDto?>
    {
        private readonly IArticleRepository _articleRepository;
        public GetArticleByIdQueryHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }
        public async Task<ArticleDto?> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _articleRepository.GetArticleByIdAsync(request.Id);
            if (result is null)
            {
                return null;
            }
            return result.Adapt<ArticleDto>();
        }
    }
}
