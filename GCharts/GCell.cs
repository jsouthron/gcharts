using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCharts
{
    /// <summary>
    /// Newtonsoft Javascript serializer will not return f when null, making it entirely optional
    /// </summary>
    public class GCell
    {
        public object v { get; set; }
        public string f { get; set; }

        public bool ShouldSerializef() { return (f != null); }
    }
}
