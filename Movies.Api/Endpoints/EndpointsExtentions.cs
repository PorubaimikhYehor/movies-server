using System;
using Movies.Api.Endpoints.Movies;
using Movies.Api.Mapping;

namespace Movies.Api.Endpoints;

public static class EndpointsExtentions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapMovieEndpoints();
        app.MapRatingEndpoints();
        return app;
    }
}
// Configure the HTTP request pipeline.