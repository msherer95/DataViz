using System;
using System.Collections.Generic;
using DataViz.Contracts;
using System.Text;
using OfficeOpenXml;
using System.IO;
using System.Linq;
using DataViz.Db;
using Microsoft.EntityFrameworkCore;

namespace DataViz.TableImport
{
    public class TableGenerator
    {
        readonly Context _context; // reference to the Db

        public TableGenerator(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Go through each table name and it's list of properties and create a schema
        /// </summary>
        public void CreateTables(Dictionary<string, List<PropertyDescriptor>> tableNameToProperties)
        {
            foreach(KeyValuePair<string, List<PropertyDescriptor>> entry in tableNameToProperties)
            {
                CreateSchema(entry.Key, entry.Value);
            }
        }

        /// <summary>
        /// Create a schema of a table and insert it into the db
        /// </summary>
        void CreateSchema(string tableName, List<PropertyDescriptor> properties)
        {
            var query = new StringBuilder($"create table if not exists \"{tableName}\" ( "); // create table with the table name
            query.Append("\"Id\" integer primary key, "); // add an Id column (TODO: will probably need to handle if an Id column already exists)

            int propIdx = 0; // track properties
            // go through each property...
            foreach (var property in properties)
            {
                query.Append($"\"{property.Name}\" {property.DbType}"); // add the property name and it's type

                // add a comma if it's not the last item
                if (propIdx < properties.Count - 1)
                {
                    query.Append(", ");
                }
                propIdx++;
            }

            query.Append(");"); // close the statement
            _context.Database.ExecuteSqlCommand(query.ToString()); // send out sql command
        }

        /// <summary>
        /// Imports the xlsx data into a pre-defined Db table. TODO: this should be moved into a separate Parsers project.
        /// </summary>
        public void ImportXlsxData(string filePath, Dictionary<string, string> sheetNameToTableName)
        {
            // Begin a SQL transaction. Inserting every row is it's own SQL command, so needs to be a part of the transaction
            // instead of a single query.
            var query = new StringBuilder("begin; ");

            using (ExcelPackage xlsxPackage = new ExcelPackage(new FileInfo(filePath)))
            {
                // go through each sheet...
                var sheets = xlsxPackage.Workbook.Worksheets;
                foreach (var sheet in sheets)
                {
                    // pull out the requested table name for this sheet
                    if (sheetNameToTableName.TryGetValue(sheet.Name, out string tableName))
                    {
                        query.AppendLine($"delete from \"{tableName}\";"); // clear the table first
                        var totalRows = sheet.Dimension.End.Row;
                        var totalCols = sheet.Dimension.End.Column;
                        ExcelRange cells = sheet.Cells;

                        // go through each row after the first (we don't want the row with column names)
                        int id = 0;
                        for (int rowIdx = 2; rowIdx <= totalRows; rowIdx++)
                        {
                            query.AppendLine($"insert into \"{tableName}\" values ("); // start the insert
                            var rowList = new List<string> { id.ToString() };

                            var selectedRow = new List<string>();
                            for (int colIdx = 1; colIdx <= totalCols; colIdx++)
                            {
                                ExcelRange cell = cells[rowIdx, colIdx];
                                string value = cell.Value == null ? "null" : String.Format("'{0}'", cell.Value.ToString());
                                selectedRow.Add(value);
                            }

                            rowList = rowList.Concat(selectedRow).ToList(); // join with Id
                            var formattedRow = string.Join(',', rowList); // join all input values with a comma
                            query.AppendLine($"{formattedRow});"); // add in entire row of data
                            id++; // increment the Id
                        }
                    }
                    else
                    {
                        throw new ArgumentException($"No table name specified for XLSX sheet: {sheet.Name}");
                    }
                }
            }

            query.AppendLine("commit;"); // complete the transaction
            _context.Database.ExecuteSqlCommand(query.ToString());
        }
    }
}
