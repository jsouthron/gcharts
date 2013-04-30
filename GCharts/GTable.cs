using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCharts
{
    /// <summary>
    /// Represents a Google Data Table.  See https://developers.google.com/chart/interactive/docs/reference#DataTable
    /// Constructors getters and setters return 'this' to faciliate fluent builder pattern
    /// </summary>
    public class GTable
    {
        /// <summary>
        /// Private field denoting whether table has been constructed with index columns.
        /// Index columns are necessary for use of the aggregate functions.  This is a 
        /// so-called "paranoid" integrity check.  In the current build, most aggregators 
        /// would function with or without the index.  It is built in for more advanced
        /// operators in future release.  
        /// </summary>
        private bool _colsIndexed = false;

        /// <summary>
        /// Data Table only has two properties, cols and rows
        /// </summary>
        public List<GColumn> cols { get; set; }
        public List<GRow> rows { get; set; }

        /// <summary>
        /// Default
        /// </summary>
        public GTable()
        {
            cols = new List<GColumn>();
            rows = new List<GRow>();
        }

        /// <summary>
        /// Overridden constructor that takes in a list of columns at creation
        /// </summary>
        /// <param name="columns"></param>
        public GTable(List<GColumn> columns)
        {
            cols = new List<GColumn>();
            cols.AddRange(columns);

            rows = new List<GRow>();
        }

        /// <summary>
        /// Adds a column of type GColumn
        /// </summary>
        /// <param name="col"></param>
        /// <returns>self</returns>
        public GTable AddColumn(GColumn col) { cols.Add(col); return this; }

        /// <summary>
        /// Indexed version.  Takes in params and creates a new column of type GColumn
        /// </summary>
        /// <param name="id"></param>
        /// <param name="label"></param>
        /// <param name="type"></param>
        /// <returns>self</returns>
        public GTable AddColumn(string id, string label, string type)
        {
            if (cols.Any(c => c.id == id)) throw new DuplicateIndexException();

            _colsIndexed = true;
            cols.Add(new GColumn { id = id, label = label, type = type });
            return this;
        }

        /// <summary>
        /// Non-indexed version.  Takes in label and type and creates a new column of type GColumn
        /// </summary>
        /// <param name="label"></param>
        /// <param name="type"></param>
        /// <returns>self</returns>
        public GTable AddColumn(string label, string type)
        {
            cols.Add(new GColumn { label = label, type = type });
            return this;
        }

        /// <summary>
        /// Adds a row
        /// </summary>
        /// <param name="row"></param>
        /// <returns>self</returns>
        public GTable AddRow(GRow row) { rows.Add(row); return this; }

        /// <summary>
        /// Adds a list of rows
        /// </summary>
        /// <param name="items"></param>
        /// <returns>self</returns>
        public GTable AddRows(List<GRow> items) { rows.AddRange(items); return this; }

        /// <summary>
        /// Total # of columns
        /// </summary>
        public int ColumnCount() { return cols.Count; }

        /// <summary>
        /// Total number of rows
        /// </summary>
        public int RowCount() { return rows.Count; }

        /// <summary>
        /// Sum of all values in the row
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Decimal ColumnSum(string id) { return TableHasIndex().GetNumericCells(id).Sum(predicate); }

        /// <summary>
        /// Average of all values in the row
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Decimal ColumnAverage(string id) { return TableHasIndex().GetNumericCells(id).Average(predicate); }

        /// <summary>
        /// Max row value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Decimal ColumnMax(string id) { return TableHasIndex().GetNumericCells(id).Max(predicate); }

        /// <summary>
        /// Min row value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Decimal ColumnMin(string id) { return TableHasIndex().GetNumericCells(id).Min(predicate); }

        private Func<object, decimal> predicate = x => decimal.Parse(x.ToString());

        private GTable TableHasIndex() { if (!_colsIndexed) throw new NonIndexedColumnsException(); return this; }

        private List<object> GetNumericCells(string id)
        {
            var index = this.GetColumnIndexById(id);
            return GetColumnValues(index).Where(x => x.GetType().IsNumericType()).ToList();
        }

        private List<object> GetColumnValues(int index)
        {
            if (index > cols.Count) throw new RowIndexOutOfBoundsException();
            return rows.Select(r => r[index].v).ToList();
        }
    }
}
