using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCharts
{
    internal class DuplicateIndexException : Exception
    {
        internal DuplicateIndexException() : base("Property: id must be unique for each column.") { }
    }

    internal class NonIndexedColumnsException : Exception
    {
        internal NonIndexedColumnsException() : base("Columns must be indexed to call aggregate methods") { }
    }

    internal class RowIndexOutOfBoundsException : Exception
    {
        internal RowIndexOutOfBoundsException() : base("Index outside the bounds of the GRow array") { }
    }

    internal class ColumnNotFoundException : Exception
    {
        internal ColumnNotFoundException() { }
        internal ColumnNotFoundException(string column_id) : base("Column of id: " + column_id + " could not be found") { }
    }

    internal class NonNumericColumnTypeException : Exception
    {
        internal NonNumericColumnTypeException() { }
        internal NonNumericColumnTypeException(string type) : base("Column GType queried is: " + type + ", Method: SumByCol() may only be used on GType.NumberType columns.") { }
    }

    internal class NonNumericCellTypeException : Exception
    {
        internal NonNumericCellTypeException() { }
        internal NonNumericCellTypeException(Type type) : base("Value of CLR type: " + type.ToString() + " found in rows.  Method: SumByCol() may only aggregate cell values with CLR numeric types (int, double, etc.)") { }
    }
}
