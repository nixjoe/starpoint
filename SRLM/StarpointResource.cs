using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SRLM {
    public class StarpointResource {
        public string name { get; set; }
        public float weight { get; set; }
        [XmlIgnore]
        public SRL library { get; set; }
        public string fqModel {
            get {
                return library.bundle + '.' + library + '.' + name;
            }
        }
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
