using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

    public class StarpointObject {
        public string name { get; set; }
        public float xScale { get; set; }
        public float yScale { get; set; }
        public float zScale { get; set; }
        [XmlIgnore]
        public SOL library { get; set; }
        public string model { get; set; }
        public string fqModel {
            get {
                return library.bundle + '.' + library.name + '.' + model;
            }
        }

        public List<Property> properties;
        public List<Operation> operations;
        public List<StarpointCollider> colliders;
        public StarpointObject() {
            name = "New Object";
            model = "Model Name.fbx";
            xScale = 1;
            yScale = 1;
            zScale = 1;
            properties = new List<Property>();
            operations = new List<Operation>();
            colliders = new List<StarpointCollider>();
        }
        public StarpointObject(SOL library) {
            this.library = library;
            name = "New Object";
            model = "Model Name.fbx";
            xScale = 1;
            yScale = 1;
            zScale = 1;
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
    public string GetObjModel() {
        return Path.GetFileNameWithoutExtension(fqModel) + ".obj";
    }
}
