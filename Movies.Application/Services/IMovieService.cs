using System;
using Movies.Application.Models;

namespace Movies.Application;

public interface IMovieService
{
    Task<bool> CreateAsync(Movie movie, CancellationToken token = default);
    Task<Movie?> GetByIdAsync(Guid id, Guid? userId = default, CancellationToken token = default);
    Task<Movie?> GetBySlugAsync(string slug, Guid? userId = default, CancellationToken token = default);
    Task<IEnumerable<Movie>> GetAllAsync(GetAllMoviesOptions options, CancellationToken token = default);
    Task<Movie?> UpdateAsync(Movie movie, Guid? userId = default, CancellationToken token = default);
    Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);
    Task<int> GetCountAsync(string? title, int? yearOfRelease, CancellationToken token = default);

}

