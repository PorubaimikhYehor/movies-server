using System;
using FluentValidation;
using Movies.Application.Models;

namespace Movies.Application.Validators;

public class GetAllMoviesOptionsValidator : AbstractValidator<GetAllMoviesOptions>
{
    private static readonly string[] AccepteableSortFields =
    {
        "title", "yearofrelease"
    };

    public GetAllMoviesOptionsValidator()
    {
        RuleFor(x => x.YearOfRelease)
            .LessThanOrEqualTo(DateTime.UtcNow.Year);

        RuleFor(x => x.SortField)
            .Must(x => string.IsNullOrEmpty(x) || AccepteableSortFields.Contains(x, StringComparer.OrdinalIgnoreCase))
            .WithMessage($"Sort field must be one of the following: {string.Join(", ", AccepteableSortFields)}");

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 25)
            .WithMessage("Page size must be between 1 and 25");
    }
}
