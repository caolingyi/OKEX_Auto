using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.Utilities
{
    public class TreeNode
    {
        public long id { get; set; }
        public long pId { get; set; }
        public string name { get; set; }
        public bool open { get; set; }
        public bool @checked { get; set; }
        public string type { get; set; }
    }
}
