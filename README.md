# Blazor Clean Architecture + CQRS with MediatR

In the evolving landscape of web development, Blazor stands out as a revolutionary framework that empowers developers to build rich, interactive web applications with the elegance of C# and the power of .NET 8. By harnessing the capabilities of Single-Page Applications (SPAs), Blazor provides a robust environment that caters to both client-side and server-side execution, ensuring fast, real-time updates and seamless user experiences. This article delves into the intricacies of Blazor, exploring its architecture, including Clean Architecture, the CQRS pattern with MediatR, and the innovative Vertical Slice Architecture that streamlines the development process.

Ask me why I love Blazor.
I love Blazor development because it is a complete Single-Page Application (SPA) UI framework for building rich, interactive web applications using .NET 8 framework and C# 12.

Blazor offers flexibility with options like WebAssembly for full client-side execution and Blazor Server for fast, real-time updates. With Server-Side Rendering (SSR), it improves performance and SEO, and Blazor Auto dynamically selects the best rendering mode. It enables C# coding everywhere, is blazing fast, and eliminates the need for JavaScript for most tasks.

Blazor Hosting Models
Blazor Server Side Rendered (SSR) Mode generates and sends static HTML from the server to the client, and the rendering happens entirely on the server. After the initial render, the client can interact with the server for more updates.

Rendering: Server-side.
Interactivity: Typically involves round-trips to the server for user interactions (like clicking buttons), unless JavaScript is used.
Performance: Faster initial load because only HTML is sent. However, user interactions may feel slower because every action requires a server round-trip.
Use Case: Suitable for pages where SEO and quick first-page load are important.
Stream Rendering allows parts of a Blazor application to be progressively rendered and sent to the client as they are completed, rather than waiting for the entire page to be processed.

Rendering: Server-side, but streamed in chunks.
Interactivity: User can start interacting with visible parts of the UI while the rest of the page is still being rendered.
Performance: Improves perceived load times by sending the initial parts of the page early. User interactivity is enabled as sections become available.
Use Case: Ideal for pages with heavy content or complex UI where rendering different parts at different times provides a smoother experience.
Blazor Server Components run on the server but update the UI in the browser via a real-time SignalR connection. These components render HTML on the server and send a “render tree” (UI diff) to the client for updates.

Rendering: Server-side, with real-time updates via SignalR.
Interactivity: Highly interactive, as the user interacts with the UI, the changes are processed on the server, and only the UI diffs are sent back to the client.
Performance: Faster interactivity compared to SSR, but requires a constant server connection and low latency for the best performance.
Use Case: Ideal for internal business applications where maintaining a connection and real-time updates are crucial, and you want to avoid downloading the full app to the client.
Blazor Web Assembly Components (WASM) run entirely in the client’s browser using WebAssembly. The components are downloaded as part of the initial application, and the rendering logic happens client-side.

Rendering: Client-side.
Interactivity: All interactions are handled client-side, which results in faster interactions because there’s no need to contact the server for each action.
Performance: Slower initial load (due to downloading the application), but very fast once the app is fully loaded in the browser. Can run offline.
Use Case: Great for applications that require rich client-side interactivity, offline capabilities, or where a persistent server connection isn’t feasible.
Auto Mode (Server to WebAssembly Transition) Blazor can start rendering components on the server (like Blazor Server) and then transition to WebAssembly on the client when the WebAssembly runtime becomes available.

Rendering: Begins with server-side rendering (SSR), then switches to client-side WebAssembly rendering as the WASM runtime is downloaded.
Interactivity: Initially, interactions are processed server-side, but after transitioning to WebAssembly, all interactivity happens on the client.
Performance: Faster initial load because the application starts as an SSR app, and then transitions to a client-side WebAssembly app to provide a more interactive experience without server round-trips.
Use Case: Suitable for apps that want the best of both worlds — fast initial loads with SSR and fast interactivity with WASM after the transition. This mode is beneficial when you want SEO support and fast interactions after the initial page load.
What is Clean Architecture?
Presentation/UI => Infrastructure => Application => Domain

Clean Architecture organizes a project in such a way that ensures separation of concerns by decoupling different aspects of the application. Clean Architecture typically includes the following Layers, which are included in the Solution as separate projects:

Domain Layer (Entities)
Inner-most layer represents the core business objects of the application. These entities capture business concepts and are independent of other layers.
References:

Project References: None
Example: In a Blazor app managing orders, an Order class would be an entity.
Application Layer (Use Cases)
Contains the business rules and defines what operations can be performed using the entities.

Project References: Domain Layer
Example: A use case for placing an order might include order validation, updating inventory, and sending notifications.
Infrastructure Layer
Implements the interfaces to interact with external systems like databases, APIs, or file systems.

Project References: Application Layer
Example: An implementation of IOrderRepository using Entity Framework or Dapper to access a SQL database.
Presentation/UI Layer
Handles user interaction and displays data using Razor Components in Blazor. This layer communicates with use cases to retrieve or modify data.

Project References: Infrastructure Layer
Example: Blazor components such as OrderList.razor or OrderDetails.razor are part of this layer, fetching and displaying order data.
CQRS + MediatR Pattern
CQRS is a software pattern that separates reading (query) and writing (command) logic, which further promotes separation of concerns and decoupling. In addition, the CQRS pattern allows us to optimize database reads and writes separately for scalability, caching and data consistency.

The Mediator Pattern is a behavioral design pattern that helps in reducinthe coupling between objects by ensuring that objects communicate with each other through a mediator instead of directly.


Command: The component sends a Command (e.g., CreateOrderCommand) to the Mediator. The Mediator routes this command to the correct Handler in the Application Layer. The Handler performs the business logic, interacting with the Domain Layer or Infrastructure Layer to persist data.
Query: If a query is made (e.g., fetching orders), a Query (e.g., GetOrdersQuery) is sent to the Mediator, which routes it to the corresponding QueryHandler that fetches data and returns it.
Example: This example creates an ICommand and ICommandHandler using MediatR and wrapping the results with OK or Fail.

public interface ICommand : IRequest<Result>
{
}
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}

// CQRS Pattern
// Command Handler for commands that do not return any specific result other than success or failure
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}
// CQRS Pattern
// Command Handler for commands that do return a specific response type
public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
}
Example: Executing a Command using ICommandHandler. This example uses Microsoft.Identity for authorization and Mapster Auto-Mapper to convert result to Data Transformation Object, which is a simple and lightweight object used to communicate between Layers.

// Article Data Transformation Object
// Used to Communicate with the Client
public record struct ArticleDto(
    int Id,
    string Title,
    string? Content,
    DateTime DatePublished,
    bool IsPublished,
    string UserName,
    string UserId,
    bool CanEdit
)
{ }

// Command Parameters
public class CreateArticleCommand : ICommand<ArticleDto>
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public DateTime DatePublished { get; set; } = DateTime.Now;
    public bool IsPublished { get; set; } = false;
}

public class CreateArticleCommandHandler : ICommandHandler<CreateArticleCommand, ArticleDto>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IUserService _userService;
    public CreateArticleCommandHandler(IArticleRepository articleRepository, IUserService userService)
    {
        _articleRepository = articleRepository;
        _userService = userService;
    }
    public async Task<Result<ArticleDto>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newArticle = request.Adapt<Article>();
            newArticle.UserId = await _userService.GetCurrentUserIdAsync();
            if (!await _userService.CurrentUserCanCreateArticleAsync())
            {
                return FailingResult("You are not authorized to create an article.");
            }
            var article = await _articleRepository.CreateArticleAsync(newArticle);
            return article.Adapt<ArticleDto>();
        }
        catch (UserNotAuthorizedException)
        {
            return FailingResult("An error occurred creating the article.");
        }

    }
    private Result<ArticleDto> FailingResult(string msg)
    {
        return Result.Fail<ArticleDto>(msg ?? "Failed to create article.");
    }
}
Vertical Slice Architecture
With Blazor development, I typically prefer a Vertical Slice Architecture, which organizes source code by feature rather than by technology. This approach promotes better separation of concerns, enhances maintainability, and allows teams to work more autonomously.


Feature-Focused: Each vertical slice contains everything needed to implement a specific feature or functionality, including UI, business logic, and data access code.
Independently Deployable: Since each slice is a self-contained unit, it can be developed, tested, and deployed independently, making continuous integration and deployment easier.
Enhanced Collaboration: Teams can work on different slices concurrently, reducing dependencies and enabling faster delivery of features.
Improved Maintainability: By grouping related code together, it becomes easier to understand and maintain the functionality associated with a specific feature.
Easier Testing: Testing can be more straightforward since each slice can be tested in isolation, ensuring that all components related to a feature are working as intended.
Example: In the Application Layer, there is a folder called Articles that handles all of the article-related functionality.


In conclusion, Blazor represents a significant leap forward in web application development, marrying the benefits of modern frameworks with the reliability of C#. Its flexible architecture, combined with patterns like Clean Architecture and CQRS, enhances maintainability and scalability, allowing developers to create high-quality applications with ease. As Blazor continues to evolve, its potential to transform the development landscape is undeniable, making it an essential tool for any developer looking to build the next generation of web applications. Embracing Blazor not only facilitates faster development cycles but also fosters a deeper connection with the underlying business logic, enabling the creation of truly impactful digital experiences.
