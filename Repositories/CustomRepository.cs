using System.Collections.Generic;
using Com.Dotnet.Cric.Data;
using Microsoft.EntityFrameworkCore;

namespace Com.Dotnet.Cric.Repositories
{
    public class CustomRepository
    {
        protected readonly AppDbContext _dbContext;

        public CustomRepository()
        {
            
        }

        public CustomRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Dictionary<string, object>> ExecuteQuery(string query)
        {
            var result = new List<Dictionary<string, object>>();
            using var command = _dbContext.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            _dbContext.Database.OpenConnection();
            
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var row = new Dictionary<string, object>();
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    var columnName = reader.GetName(i);
                    var columnValue = reader.GetValue(i);
                    row[columnName] = columnValue;
                }
                result.Add(row);
            }

            return result;
        }
    }
}