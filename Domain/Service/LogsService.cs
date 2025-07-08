using Domain.DTO.Logs;
using Domain.Interface;
using Domain.Repositories;
using Entity;
using Entity.Context;
using Microsoft.Data.Sqlite;
using SharedKernel;

namespace Domain.Service
{
    public class LogsService : ILogsService
    {
        private readonly AppDbContext _context;

        public LogsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DataTable<LogsData>> GetLogsDataTable(LogsFilter filter)
        {
            var result = new DataTable<LogsData>();
            result.Data = new List<LogsData>();
            var logs = new List<LogsData>();


            using (var connection = new SqliteConnection("Data Source=Logs.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                                       SELECT 
                                           id,
                                           Timestamp,
                                           Level,
                                           Exception,
                                           RenderedMessage,
                                           Properties,
                                           (SELECT COUNT(*) FROM Logs) AS TotalCount
                                       FROM Logs
                                       ORDER BY Timestamp DESC
                                       LIMIT @PageSize OFFSET @Offset;";

                command.Parameters.AddWithValue("@PageSize", filter.PageSize);
                command.Parameters.AddWithValue("@Offset", filter.PageIndex * filter.PageSize);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var log = new LogsData
                        {
                            Id = reader.GetInt32(0),
                            Timestamp = reader.GetString(1),
                            Level = reader.GetString(2),
                            Exception = reader.IsDBNull(3) ? null : reader.GetString(3),
                            RenderedMessage = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Properties = reader.IsDBNull(5) ? null : reader.GetString(5),
                        };

                        logs.Add(log);

                        if (result.Count == 0 && !reader.IsDBNull(6))
                        {
                            result.Count = reader.GetInt32(6);
                        }
                    }
                }

                await connection.CloseAsync();
                await connection.DisposeAsync();
            }

            result.Data = logs;

            return result;
        }
    }
}
