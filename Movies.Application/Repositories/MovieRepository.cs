using System;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using Dapper;
using Movies.Application.Database;
using Movies.Application.Models;

namespace Movies.Application.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    public MovieRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }
    private readonly List<Movie> _movies = new();
    public async Task<bool> CreateAsync(Movie movie)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var result = await connection.ExecuteAsync(new CommandDefinition("""
        insert into movies (id, slug, title, yearofrelease)
        values (@Id, @Slug, @Title, @YearOfRelease)
        """, movie));

        if (result > 0)
        {
            foreach (var genre in movie.Genres)
            {
                await connection.ExecuteAsync(new CommandDefinition("""
                insert into genres (movieid, name)
                values (@MovieId, @Name)
                """, new { MovieId = movie.Id, Name = genre}));
            }
        }
        transaction.Commit();

        return result > 0;

    }
    public Task<Movie?> GetByIdAsync(Guid id)
    {
        var movie = _movies.SingleOrDefault(i => i.Id == id);
        return Task.FromResult(movie);
    }
    public Task<Movie?> GetBySlugAsync(string slug)
    {
        var movie = _movies.SingleOrDefault(i => i.Slug == slug);
        return Task.FromResult(movie);
    }
    public Task<IEnumerable<Movie>> GetAllAsync()
    {
        return Task.FromResult(_movies.AsEnumerable());
    }
    public Task<bool> UpdateAsync(Movie movie)
    {
        int movieIndex = _movies.FindIndex(i => i.Id == movie.Id);
        if (movieIndex == -1)
        {
            return Task.FromResult(false);
        }
        else
        {
            _movies[movieIndex] = movie;
            return Task.FromResult(true);
        }
    }
    public Task<bool> DeleteByIdAsync(Guid id)
    {
        int removedCount = _movies.RemoveAll(i => i.Id == id);
        bool movieRemoved = removedCount > 0;
        return Task.FromResult(movieRemoved);
    }
    public Task<bool> ExistsByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
