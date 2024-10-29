using BlazingBlog.Application.Users;

namespace BlazingBlog.Application.Articles.GetArticles
{
	public class GetArticlesByCurrentUserQueryHandler : IQueryHandler<GetArticlesByCurrentUserQuery, List<ArticleDto>>
	{
		private readonly IArticleRepository _articleRepository;
		private readonly IUserService _userService;
		public GetArticlesByCurrentUserQueryHandler(IArticleRepository articleRepository, IUserService userService)
		{
			_articleRepository = articleRepository;
			_userService = userService;
		}
		public async Task<Result<List<ArticleDto>>> Handle(GetArticlesByCurrentUserQuery request, CancellationToken cancellationToken)
		{
			var userId = await _userService.GetCurrentUserIdAsync();
			var result = await _articleRepository.GetArticlesByUserAsync(userId);
			var response = result.Adapt<List<ArticleDto>>();
			return response.OrderByDescending(e => e.DatePublished).ToList();
		}
	}
}
