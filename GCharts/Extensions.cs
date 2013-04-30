using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCharts
{
    /// <summary>
    /// Useful for populating the type column in the GTable without resorting to string memorization
    /// </summary>
    public class GTypes
    {
        public const string StringType = "string";
        public const string NumberType = "number";
        public const string BoolType = "boolean";
        public const string DateType = "date";
        public const string DateTimeType = "datetime";
        public const string TimeOfDayType = "timeofday";
    }

    /// <summary>
    /// Plenty of handy methods
    /// </summary>
    public static class GoogleExtensionMethods
    {
        /// <summary>
        /// Formats DateTime object to Javascript Date() object expected by Google Chart JS API
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToGChartDate(this DateTime dt)
        {
            var dFormat = "Date({0}, {1}, {2})";
            return string.Format(dFormat, dt.Year, dt.Month - 1, dt.Day);
        }

        /// <summary>
        /// Formats DateTime object to Javascript Date() object, including hours minutes and seconds, expected by Google Chart JS API
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToGChartDateTime(this DateTime dt)
        {
            var dFormat = "Date({0}, {1}, {2}, {3}, {4}, {5})";
            return string.Format(dFormat, dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
        }

        /// <summary>
        /// Returns current column index in GTable cols collection
        /// </summary>
        /// <param name="table"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static int GetColumnIndex(this GTable table, GColumn col)
        {
            return table.cols.IndexOf(col);
        }

        /// <summary>
        /// Returns current column index in GTable cols collection by column id
        /// </summary>
        /// <param name="table"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static int GetColumnIndexById(this GTable table, string id)
        {
            var _column = table.cols.Where(c => c.id == id).FirstOrDefault();
            if (_column == null)
                throw new ColumnNotFoundException(id);

            return table.GetColumnIndex(_column);
        }

        /// <summary>
        /// For a given string based column id, returns a strongly typed column object
        /// </summary>
        /// <param name="table"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static GColumn GetColumnById(this GTable table, string id)
        {
            var _column = table.cols.Where(c => c.id == id).FirstOrDefault();
            if (_column == null)
                throw new ColumnNotFoundException(id);

            _column.TableIndex = table.GetColumnIndex(_column);

            return _column;
        }

        /// <summary>
        /// Used in the GTable aggregators to determine if cell values are CLR numeric types
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNumericType(this Type type)
        {
            if (type == null)
            {
                return false;
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
                case TypeCode.Object:
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        return IsNumericType(Nullable.GetUnderlyingType(type));
                    }
                    return false;
            }
            return false;
        }
    }
}
