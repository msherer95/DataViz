using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataViz.Contracts;
using DataViz.Db;
using DataViz.TableImport;
using System.IO;
using DataViz.Query;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DataViz
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : Controller
    {
        readonly Context _context;

        public ValuesController(Context context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }   

        [HttpPost("QueryRequest")]
        public ActionResult<QueryRequest> QueryRequest()
        {
            var a = new QueryRequest()
            {
                Id = _context.QueryRequests.Count() + 1000,
                TableName = "SomeTable",
                Categories = new QueryCategories()
                {
                    Columns = new List<string>() { "c", "d"}
                }
            };

            _context.QueryRequests.Add(a);
            _context.SaveChanges();

            return _context.QueryRequests.Include(x => x.Categories).First();
        }

        [HttpPost("TestReader")]
        public ActionResult<TableResult> TestReader()
        {
           return _context.SqlReadQuery("select * from \"QueryRequests\";");
        }

        // Currently being used as a post method, but will be moved into a unit test
        [HttpPost("CreateTable")]
        public ActionResult<TableResult> CreateTable([FromBody] Dictionary<string, List<PropertyDescriptor>> tableNameToProperties)
        {
            try
            {
                var tb = new TableGenerator(_context);
                tb.CreateTables(tableNameToProperties);
                tb.ImportXlsxData("../temp/testfile.xlsx", new Dictionary<string, string>() {
                    {"Sheet1", "MyTable"},
                    {"Sheet2", "AnotherTable"}
                });

                var req = new QueryRequest()
                {
                    XCol = "\"Age\"",
                    YCols = new List<string> { "\"Height\"" },
                    Filters = "where \"Age\" > 20",
                    TableName = "\"MyTable\"",
                    Categories = new QueryCategories()
                    {
                        Columns = new List<string> { "Name" },
                        Conditionals = new Dictionary<string, string>()
                        {
                            {"\"Height\" > 20", "tall"},
                            {"\"Height\" < 10", "short"}
                        }
                    },
                    Functions = new Dictionary<string, string>()
                    {
                        {"SomeFn", "(\"Age\" * \"Height\")/2"}
                    },
                    Skip = 0,
                    Take = 25
                };

                string query = new QueryGenerator(_context).Generate(req);
               return _context.SqlReadQuery(query);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
