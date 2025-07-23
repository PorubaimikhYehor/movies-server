using System;

namespace Movies.Api.Mapping;

public class MovieRating
{
    public required Guid MovieId { get; init; }
    public required string Slug { get; init; }
    public int Rating { get; init; }
}
