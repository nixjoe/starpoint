using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLM {
    public class SOL {
        public List<StarpointObject> objects;
        public string name { get; set; }
        public string bundle { get; set; }
        public string fqName {
            get {
                return bundle + "." + name + ".sol";
            }
        }

        public SOL() {
            objects = new List<StarpointObject>();
        }
    }
}
