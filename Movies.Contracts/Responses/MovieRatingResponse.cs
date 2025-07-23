using System;

namespace Movies.Contracts.Responses;

public class MovieRatingResponse
{
    public required Guid MovieId { get; init; }
    public required string Slug { get; init; }
    public int Rating { get; init; }
}
