using System;
using System.Text.RegularExpressions;

namespace Movies.Application.Models;

public partial class Movie
{
    public required Guid Id { get; init; }
    public required string Title { get; set; }
    public required int YearOfRelease { get; set; }
    public float? Rating { get; set; }
    public int? UserRating { get; set; }
    public string Slug => GenerateSlug();
    public required List<string> Genres { get; init; } = new();

    private string GenerateSlug()
    {
        string slugedTitle = SlugRegex().Replace(Title, string.Empty).ToLower().Replace(" ", "-");
        return $"{slugedTitle}-{YearOfRelease}";
    }
    [GeneratedRegex("[^0-9A-Za-z _-]", RegexOptions.NonBacktracking, 5)]
    private static partial Regex SlugRegex();
}