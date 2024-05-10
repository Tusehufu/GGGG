using InternJohan.Dev.Infrastructure.Configuration;
using InternJohan.Dev.Infrastructure.Models;
using Microsoft.Extensions.Options;
using Dapper;
using Microsoft.Data.SqlClient;

namespace InternJohan.Dev.Infrastructure.Repository
{
    public class MovieRepository
    {
        private readonly DatabaseSettings _databaseSettings;

        public MovieRepository(IOptions<DatabaseSettings> databaseSettings)
        {
            _databaseSettings = databaseSettings.Value;
        }

        public async Task<IEnumerable<Movie>> FindAll()
        {
            IEnumerable<Movie> items;
            using (var connection = new SqlConnection(_databaseSettings.DefaultConnection))
                items = await connection.QueryAsync<Movie>(@"
                    SELECT * FROM Movies
                   -- ORDER BY ReleaseYear DESC
                ");

            return items;
        }
        public async Task<bool> Insert(Movie movie)
        {
            try
            {
                using var connection = new SqlConnection(_databaseSettings.DefaultConnection);
                movie.Id = await connection.ExecuteScalarAsync<int>(@"
                    DECLARE @InsertedMovie TABLE (Id INT);

                    INSERT INTO [Movies] (
                        [Title],
                        [ReleaseYear],
						AverageRating
                    )
                    OUTPUT INSERTED.Id INTO @InsertedMovie
                    VALUES (
                        @Title,
                        @ReleaseYear,
                        @AverageRating
                    );

                    SELECT Id FROM @InsertedMovie;
                ", movie);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                using var connection = new SqlConnection(_databaseSettings.DefaultConnection);
                var affectedRows = await connection.ExecuteAsync(@"
                    DELETE FROM [Movies]
                    WHERE [Id] = @Id
                ", new { Id = id });

                return affectedRows > 0;
            }
            catch
            {
                return false;
            }
        }
        public async Task<Movie> FindById(int id)
        {
            using (var connection = new SqlConnection(_databaseSettings.DefaultConnection))
                return await connection.QueryFirstOrDefaultAsync<Movie>(@"
                    SELECT *
                    FROM MOVIES
                    WHERE Id =  @Id
            ", new { Id = id });
        }
        public async Task<bool> Update(Movie updatedMovie)
        {
            using (var connection = new SqlConnection(_databaseSettings.DefaultConnection))
            {
                var query = @"
            UPDATE MOVIES
            SET Title = @Title,
                ReleaseYear = @ReleaseYear,
                AverageRating = @AverageRating
            WHERE Id = @Id
        ";

                await connection.ExecuteAsync(query, updatedMovie);
                return true;

            }
        }
    }
}
