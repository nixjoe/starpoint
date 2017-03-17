using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLM {
    public class PropertyEnumValue {
        public string name { get; set; }
        public int value { get; set; }
        public PropertyEnumValue() { }
        public PropertyEnumValue(string name, int value) {
            this.name = name;
            this.value = value;
        }
    }
}
