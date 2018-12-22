using System;
using System.Collections.Generic;
using DataViz.Contracts;
using DataViz.Db;
using DataViz.TableImport;
using Microsoft.AspNetCore.Mvc;

namespace DataViz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableGenerationController : Controller
    {
        private readonly Context _context;
        public TableGenerationController(Context context) 
        {
            _context = context;
        }

        [HttpPost("CreateTable")]
        public void CreateTable([FromBody] Dictionary<string, List<PropertyDescriptor>> tableNameToProperties)
        {
            var tb = new TableGenerator(_context);
            tb.CreateTables(tableNameToProperties);
        }

        [HttpPost("ImportXlsx")]
        public void ImportXlsx([FromBody] Dictionary<string, string> SheetToTable, [FromQuery] string fileName)
        {
            var tb = new TableGenerator(_context);
            tb.ImportXlsxData($"../temp/{fileName}", SheetToTable);
        }

        [HttpPost("UpdateTable")]
        public void UpdateTable([FromBody] Dictionary<string, List<PropertyDescriptor>> tableNameToProperties)
        {
            var tb = new TableGenerator(_context);
            tb.UpdateTables(tableNameToProperties);
        }
    }
}
