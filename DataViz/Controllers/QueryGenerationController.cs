using System;
using DataViz.Contracts;
using DataViz.Db;
using DataViz.Query;
using Microsoft.AspNetCore.Mvc;

namespace DataViz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryGenerationController : Controller
    {
        private readonly Context _context;
        public QueryGenerationController(Context context)
        {
            _context = context;
        }

        [HttpPost("GetChartData")]
        public TableResult GetChartData([FromBody] QueryRequest req)
        {
            var qg = new QueryGenerator(_context);
            string query = qg.Generate(req);
            return _context.SqlReadQuery(query);
        }

        [HttpPost("GetTableData")]
        public TableResult GetTableData([FromQuery] string tableName, [FromQuery] int skip, [FromQuery] int take)
        {
            string query = $"SELECT * FROM \"{tableName}\" LIMIT {take} OFFSET {skip}";
           return _context.SqlReadQuery(query);
        }
    }
}
