using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLM {
    public class StarpointResource {
        public string name;
        public float weight;
        public StarpointResource() {
            name = "";
            weight = 0.00f;
        }
        public StarpointResource(string name, float weight) {
            this.name = name;
            this.weight = weight;
        }
    }
}
