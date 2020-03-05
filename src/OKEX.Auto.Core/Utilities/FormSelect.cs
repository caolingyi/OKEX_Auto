using System;
using System.Collections.Generic;
using System.Text;

namespace OKEX.Auto.Core.Utilities
{
    public class FormSelect
    {
        public string name { get; set; }
        public long value { get; set; }

        public string type { get; set; }

        public List<FormSelect> children { get; set; }
    }
}
