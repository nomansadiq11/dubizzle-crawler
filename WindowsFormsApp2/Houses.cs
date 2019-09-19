using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    [DebuggerDisplay("")]
    public class Houses
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public string Price { get; set; }
        public int sqft { get; set; }
        public string LinkAdd { get; set; }



    }
}
