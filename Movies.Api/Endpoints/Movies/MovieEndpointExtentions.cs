using System;

namespace Movies.Api.Endpoints.Movies;

public static class MovieEndpointExtentions
{
    public static IEndpointRouteBuilder MapMovieEndpoints(this IEndpointRouteBuilder app)
    {
        // Define movie-related endpoints here
        // Example: app.MapGet("/movies", async context => { ... });
        app.MapGetMovie();
        app.MapCreateMovie();
        app.MapGetAllMovies();
        app.MapUpdateMovie();
        app.MapDeleteMovie();
        /*
         */
        return app;
    }
}
