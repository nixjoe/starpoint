using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLM {
    class StarpointObject {
        public string name { get; set; }
        public float dryWeight { get; set; }
        public StarpointObject() {
            name = "NewObject";
            dryWeight = 0;
        }
        public StarpointObject(StarpointObject copySource) {
            name = copySource.name;
            dryWeight = copySource.dryWeight;
        }
    }
}
