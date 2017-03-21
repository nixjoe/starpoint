using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLM {
    public class StarpointObject {
        public string name { get; set; }
        public float dryWeight { get; set; }
        public string model { get; set; }
        public List<Property> properties;
        public List<Operation> operations;
        public List<StarpointCollider> colliders;
        public StarpointObject() {
            name = "New Object";
            model = "Model Name.fbx";
            dryWeight = 0;
            properties = new List<Property>();
            operations = new List<Operation>();
            colliders = new List<StarpointCollider>();
        }
        public StarpointObject(StarpointObject copySource) {
            name = copySource.name;
            dryWeight = copySource.dryWeight;
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
