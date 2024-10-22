using BlazingBlog.Domain.Articles;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingBlog.Application.Articles.DeleteArticle
{
    public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, bool>
    {
        private readonly IArticleRepository _articleRepository;
        public DeleteArticleCommandHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }
        public async Task<bool> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            return await _articleRepository.DeleteArticleAsync(request.Id);
        }
    }
}
