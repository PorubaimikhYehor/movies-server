using System;
using Movies.Api.Endpoints.Ratings;

namespace Movies.Api.Mapping;

public static class RatingEndpointExtentions
{
    public static IEndpointRouteBuilder MapRatingEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapDeleteRating();
        app.MapRateMovie();
        app.MapGetUserRatings();
        return app;
    }

}
