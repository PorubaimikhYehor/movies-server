using System;
using Movies.Api.Auth;
using Movies.Api.Mapping;
using Movies.Application.Services;

namespace Movies.Api.Endpoints.Ratings;

public static class GetUserRatingsEndpoint
{
    public const string Name = "GetUserRatings";
    public static IEndpointRouteBuilder MapGetUserRatings(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Ratings.GetUserRatings, async (
            HttpContext context,
            IRatingService ratingService,
            CancellationToken token) =>
        {
            var userId = context.GetUserId();
            var ratings = await ratingService.GetRatingsForUserAsync(userId!.Value, token);
            var ratingsResponse = ratings.MapToResponse();
            return TypedResults.Ok(ratingsResponse);
        })
        .WithName(Name)
        // .Produces<MovieDto>(StatusCodes.Status200OK)
        // .Produces(StatusCodes.Status404NotFound)
        .RequireAuthorization()
        ;

        return app;
    }
}
