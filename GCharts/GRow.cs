using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCharts
{
    /// <summary>
    /// Represents a row in a Google Data Table
    /// </summary>
    public class GRow
    {
        /// <summary>
        /// single property, contains a list of GCells
        /// </summary>
        public List<GCell> c { get; set; }

        /// <summary>
        /// Default
        /// </summary>
        public GRow() { c = new List<GCell>(); }

        /// <summary>
        /// Indexer useful for enumerating or index accessing cells within a row
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public GCell this[int index]
        {
            get { return c[index] as GCell; }
            set { c[index] = value; }
        }

        /// <summary>
        /// Adds cell of Type T.  Since the cell stores value as an object, this type parameter is of 
        /// no use except for clarity in the client application
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public GRow AddCell<T>(object obj)
        {
            c.Add(new GCell { v = (T)obj });
            return this;
        }

        /// <summary>
        /// Adds cell of Type T.  Since the cell stores value as an object, this type parameter is of 
        /// no use except for clarity in the client application
        /// Also takes in a formatted string for diplay in the table.  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public GRow AddCell<T>(object obj, string format)
        {
            c.Add(new GCell { v = (T)obj, f = format });
            return this;
        }

        /// <summary>
        /// Adds cell of Type T.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public GRow AddCell(object obj) { c.Add(new GCell { v = obj }); return this; }

        /// <summary>
        /// Adds cell of Type T.
        /// Also takes in a formatted string for diplay in the table.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public GRow AddCell(object obj, string format) { c.Add(new GCell { v = obj, f = format }); return this; }
    }
}
