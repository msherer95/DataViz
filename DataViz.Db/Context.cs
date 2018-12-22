using System;
using Microsoft.EntityFrameworkCore;
using DataViz.Contracts;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Data.Common;
using System.Collections.ObjectModel;

namespace DataViz.Db
{
    // Creates EF's connection to the Db
    public class Context : DbContext
    {
        public DbSet<QueryRequest> QueryRequests { get; set; }

        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=localhost;Database=dataviz;Username=admin;Password=admin", b => b.MigrationsAssembly("DataViz"));
        }

        public TableResult SqlReadQuery(string query)
        {
            using (var command = Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                Database.OpenConnection();

                var table = new TableResult();
                using (DbDataReader result = command.ExecuteReader())
                {
                    var rowList = new List<List<object>>();
                    int rowCount = 0;

                    while (result.Read())
                    {
                        var row = new List<object>();
                        for (int fieldIdx = 0; fieldIdx < result.FieldCount; fieldIdx++)
                        {
                            row.Add(result[fieldIdx]);
                        }

                        rowCount++;
                        rowList.Add(row);
                    }

                    table.Rows = rowList;
                    table.ResultCount = rowCount;

                    var columnList = new List<string>();
                    ReadOnlyCollection<DbColumn> columnSchema = result.GetColumnSchema();
                    for (int fieldIdx = 0; fieldIdx < result.FieldCount; fieldIdx++)
                    {
                        columnList.Add(columnSchema[fieldIdx].BaseColumnName);
                    }

                    table.Columns = columnList;
                }

                Database.CloseConnection();
                return table;
            }
        }
    }
}
