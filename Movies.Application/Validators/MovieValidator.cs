using System;
using FluentValidation;
using Movies.Application.Models;
using Movies.Application.Repositories;
using Movies.Application.Services;

namespace Movies.Application.Validators;

public class MovieValidator : AbstractValidator<Movie>
{
    private readonly IMovieRepository _movieRepository;
    public MovieValidator(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;

        RuleFor(x => x.Id)
        .NotEmpty();

        RuleFor(x => x.Genres)
        .NotEmpty();

        RuleFor(x => x.Title)
        .NotEmpty();

        RuleFor(x => x.YearOfRelease)
        .LessThanOrEqualTo(DateTime.UtcNow.Year);

        RuleFor(x => x.Slug)
        .MustAsync(ValidateSlug)
        .WithMessage("This movie already exists in the system");

    }

    private async Task<bool> ValidateSlug(Movie movie, string slug, CancellationToken token = default)
    {
        var existingMovie = await _movieRepository.GetBySlugAsync(slug, null, token);
        return existingMovie is null || existingMovie.Id == movie.Id;
    }
}
