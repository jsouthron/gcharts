using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCharts
{
    /// <summary>
    /// 
    /// </summary>
    public class GColumn
    {
        public string id { get; set; }
        public string label { get; set; }
        public string type { get; set; }
        public int TableIndex { get; set; }

        public bool ShouldSerializeTableIndex() { return false; }
        public bool ShouldSerializeid() { return string.IsNullOrEmpty(id); }
    }
}
