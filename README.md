gcharts
=======

C# library classes for Google Data Tables including JSON serialization for MVC Web API

How to use
==========

(1) Simply start out by creating a table:

    var table = new GTable();

(2) Add some columns:

    table.AddColumn("Name", "Name", GTypes.StringType); 
    table.AddColumn("Age", "Age", GTypes.NumberType); 
    table.AddColumn("Can Drive", "Can Drive", GTypes.BoolType); 

(3) Add some rows:

    var row1 = new GRow()
        .AddCell<String>("Joe")
        .AddCell<Int32>(30)
        .AddCell<Boolean>(true));
                    
    var row 2 = new GRow()
        .AddCell<String>("John")
        .AddCell<Int32>(15)
        .AddCell<Boolean>(false));
      
    table.AddRow(row1);
    table.AddRow(row2);

(4) Done!

Fluent Builders
===============

All methods are chainable, so the above example can be written in one command like so:

    var table = new GTable()
        .AddColumn("Name", "Name", GTypes.StringType)
        .AddColumn("Age", "Age", GTypes.NumberType)
        .AddColumn("Can Drive", "Can Drive", GTypes.BoolType)
        .AddRow(new GRow().AddCell<String>("Joe")
                            .AddCell<Int32>(30)
                            .AddCell<Boolean>(true))
        .AddRow(new GRow().AddCell<String>("John")
                            .AddCell<Int32>(15)
                            .AddCell<Boolean>(false));
                        
Column Aggregators
==================

Total lines can easily be added, even though the Google Charts can handle this nicely as well:

    table.AddRow(new GRow()
        .AddCell<String>("TOTAL: ")
        .AddCell<Decimal>(table.ColumnSum("Age")));
    
Column Types
============

All the Google Data Table column types are convienantly stored as constants:

    public class GTypes
    {
        public const string StringType = "string";
        public const string NumberType = "number";
        public const string BoolType = "boolean"; 
        public const string DateType = "date"; 
        public const string DateTimeType = "datetime"; 
        public const string TimeOfDayType = "timeofday"; 
    }

Extension Methods
=================

Handy extension methods are built in to convert .NET Date/Time objects into Javascript Date() objects during serialization

    public static string ToGChartDate(this DateTime dt)
    {
        var dFormat = "Date({0}, {1}, {2})";
        return string.Format(dFormat, dt.Year, dt.Month-1, dt.Day);
    }

    public static string ToGChartDateTime(this DateTime dt)
    {
        var dFormat = "Date({0}, {1}, {2}, {3}, {4}, {5})";
        return string.Format(dFormat, dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
    }


Feel free to fork, comment, submit bugs or exceptions.
