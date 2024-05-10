using InternJohan.Dev.Infrastructure.Configuration;
using InternJohan.Dev.Infrastructure.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace InternJohan.Dev.Infrastructure.Repository
{
    public class SportEventRepository
    {
        private readonly DatabaseSettings _databaseSettings;

        public SportEventRepository(IOptions<DatabaseSettings> databaseSettings)
        {
            _databaseSettings = databaseSettings.Value;
        }

        public async Task<IEnumerable<SportEvent>> FindAll()
        {
            IEnumerable<SportEvent> items;
            using (var connection = new SqlConnection(_databaseSettings.DefaultConnection))
                items = await connection.QueryAsync<SportEvent>(@"
                    SELECT 
                        se.*, 
                        u.* 
                    FROM 
                        SportEvents se 
                    JOIN 
                        Users u ON se.UserHostId = u.Id
                ");

            return items;
        }

        public async Task<SportEvent> FindById(int id)
        {
            using (var connection = new SqlConnection(_databaseSettings.DefaultConnection))
                return await connection.QueryFirstOrDefaultAsync<SportEvent>(@"
                    SELECT 
                        se.*, 
                        u.* 
                    FROM 
                        SportEvents se
                    JOIN 
                        Users u ON se.UserHostId = u.Id
                    WHERE 
                        se.Id = @Id
                ", new { Id = id });
        }

        public async Task<bool> Insert(SportEvent sportEvent)
        {
            try
            {
                using var connection = new SqlConnection(_databaseSettings.DefaultConnection);
                sportEvent.Id = await connection.ExecuteScalarAsync<int>(@"
                    DECLARE @InsertedSportEvent TABLE (Id INT);

                    INSERT INTO SportEvents (
                        Sport,
                        NeededParticipants,
                        Participants,
                        FormattedDateTime,
                        Location,
                        UserHostId
                    )
                    OUTPUT INSERTED.Id INTO @InsertedSportEvent
                    VALUES (
                        @Sport,
                        @NeededParticipants,
                        @Participants,
                        @DateTime,
                        @Location,
                        @UserHostId
                    );

                    SELECT Id FROM @InsertedSportEvent;
                ", new
                {
                    sportEvent.Sport,
                    sportEvent.NeededParticipants,
                    sportEvent.Participants,
                    DateTime = sportEvent.DateTime, // Om datumet formateras, hantera det här
                    sportEvent.Location,
                    UserHostId = sportEvent.UserHost.Id
                });

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(SportEvent sportEvent)
        {
            try
            {
                using var connection = new SqlConnection(_databaseSettings.DefaultConnection);
                var query = @"
                    UPDATE SportEvents
                    SET
                        Sport = @Sport,
                        NeededParticipants = @NeededParticipants,
                        Participants = @Participants,
                        FormattedDateTime = @DateTime,
                        Location = @Location,
                        UserHostId = @UserHostId
                    WHERE Id = @Id
                ";

                await connection.ExecuteAsync(query, sportEvent);
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
                    DELETE FROM SportEvents
                    WHERE Id = @Id
                ", new { Id = id });

                return affectedRows > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}


