using System;
using Movies.Api.Auth;
using Movies.Api.Mapping;
using Movies.Application;
using Movies.Contracts.Requests;
using Movies.Contracts.Responses;

namespace Movies.Api.Endpoints.Movies;

public static class CreateMovieEndpoint
{
    public const string Name = "CreateMovie";
    public static IEndpointRouteBuilder MapCreateMovie(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.Movies.Create, async (
            CreateMovieRequest request,
            IMovieService movieService,
            CancellationToken token
            ) =>
        {
            var movie = request.MapToMovie();
            await movieService.CreateAsync(movie, token);
            var response = movie.MapToResponse();
            return TypedResults.CreatedAtRoute(response, GetMovieEndpoint.Name, new { idOrSlug = movie.Id });

        })
        .WithName(Name)
        .Produces<MovieResponse>(StatusCodes.Status201Created)
        .Produces<ValidationFailureResponse>(StatusCodes.Status404NotFound)
        .RequireAuthorization(AuthConstants.TrustedMemberPolicyName)
        ;

        return app;
    }

}
