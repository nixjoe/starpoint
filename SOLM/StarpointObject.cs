using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLM {
    public class StarpointObject {
        public string name { get; set; }
        public string model { get; set; }
        public List<Property> properties;
        public List<Operation> operations;
        public List<StarpointCollider> colliders;
        public StarpointObject() {
            name = "New Object";
            model = "Model Name.fbx";
            properties = new List<Property>();
            properties.Add(new RealProperty("hp", true, "The hit points of this object.", 0f, 1, 1));
            properties.Add(new RealProperty("armor", true, "The maximum damage this object can block before it takes damage to hit points.", 0f, null, 0));
            properties.Add(new RealProperty("mass", true, "The mass of this object.", 0f, null, 0));
            properties.Add(new RealProperty("max temperature", true, "The maximum temperature this object can reach before it is destroyed.", 0f, null, 0));
            operations = new List<Operation>();
            colliders = new List<StarpointCollider>();
        }
        public StarpointObject(StarpointObject copySource) {
            name = copySource.name;
            model = copySource.model;
            properties = new List<Property>();
            foreach (Property p in copySource.properties) {
                properties.Add(new Property(p));
            }
            operations = new List<Operation>();
            foreach (Operation o in copySource.operations) {
                operations.Add(new Operation(o));
            }
            colliders = new List<StarpointCollider>();
            foreach (StarpointCollider sc in copySource.colliders) {
                colliders.Add(new StarpointCollider(sc));
            }
        }
    }
}
