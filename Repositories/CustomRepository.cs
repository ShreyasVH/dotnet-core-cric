using System;
using System.Collections.Generic;
using System.Data;
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

        public List<Dictionary<string, object>> ExecuteQuery(
            string query,
            Dictionary<string, object>? parameters = null)
        {
            var result = new List<Dictionary<string, object>>();

            var connection = _dbContext.Database.GetDbConnection();
            var shouldClose = connection.State != ConnectionState.Open;

            if (shouldClose)
            {
                connection.Open();
            }

            try
            {
                using var command = connection.CreateCommand();
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        var dbParameter = command.CreateParameter();
                        dbParameter.ParameterName = parameter.Key;
                        dbParameter.Value = parameter.Value ?? DBNull.Value;
                        command.Parameters.Add(dbParameter);
                    }
                }

                using var reader = command.ExecuteReader();

                var fieldCount = reader.FieldCount;
                var columnNames = new string[fieldCount];
                for (var i = 0; i < fieldCount; i++)
                {
                    columnNames[i] = reader.GetName(i);
                }

                while (reader.Read())
                {
                    var row = new Dictionary<string, object>(fieldCount, StringComparer.OrdinalIgnoreCase);
                    for (var i = 0; i < fieldCount; i++)
                    {
                        row[columnNames[i]] = reader.IsDBNull(i) ? null! : reader.GetValue(i);
                    }

                    result.Add(row);
                }
            }
            finally
            {
                if (shouldClose && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return result;
        }
    }
}