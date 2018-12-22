using System;
using DataViz.Db;
using DataViz.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataViz.Query
{
    public class QueryGenerator
    {
        readonly Context _context; // reference to Db

        public QueryGenerator(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates the query from a request object.
        /// </summary>
        public string Generate(QueryRequest req)
        {
            // Get all the columns we need to request. This includes X, Y, and category columns.
            // To differentiate categorical columns, we add __CAT__ to the beginning

            KeyValuePair<string, string> xCol = req.XCol.First();

            var allCols = new List<string>() { $"{xCol.Key} as \"{xCol.Value}\"" };
            List<string> modifiedCategoryColumns = req.Categories.Columns.Select(col => $"\"{col}\" as \"{col}\"").ToList();
            allCols = allCols.Concat(req.YCols.Select(y => $"{y.Key} as \"{y.Value}\"")).Concat(modifiedCategoryColumns).ToList();

            // Add each function as another column in the SELECT.
            foreach(KeyValuePair<string, string> fn in req.Functions)
            {
                string newCol = $" {fn.Value} as {fn.Key}";
                allCols.Add(newCol);
            }

            // join with commas and select
            string allColsCommaSep = string.Join(',', allCols);
            var query = new StringBuilder($"select {allColsCommaSep}");

            // create case when statements for any conditional categories
            query.AppendLine(", CASE ");    
            foreach (KeyValuePair<string, string> consWithNames in req.Categories.Conditionals)
            {
                query.AppendLine($" WHEN {consWithNames.Key} THEN '{consWithNames.Value}'");
            }

            query.AppendLine("END"); // terminate the case when
            query.AppendLine($" FROM {req.TableName} {req.Filters}"); // choose the table and add a filter

            if (req.Groups.Count > 0)
            {
                string aggregateFns = string.Join(',', req.AggregateFunctions.Select(f => $"{f.Key} as \"{f.Value}\""));
                string groups = string.Join(',', req.Groups);
                return $"SELECT {aggregateFns}, {groups} FROM ({query}) as \"x\" GROUP BY {groups}";
            }

            // pagination
            query.AppendLine($"LIMIT {req.Take}");
            query.AppendLine($"OFFSET {req.Skip}");
            return query.ToString();
        }
    }
}
