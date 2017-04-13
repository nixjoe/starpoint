using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SOLM {
    [XmlInclude(typeof(IntegerProperty)), XmlInclude(typeof(ContainerProperty)), XmlInclude(typeof(RealProperty)), XmlInclude(typeof(EnumProperty))]
    public class Property {
        public string name { get; set; }
        public bool visible { get; set; }
        public bool control { get; set; }
        public string description;
        public enum PropertyType { NULL, Container, Integer, Real, Enum };
        public PropertyType type {
            get {
                if (this is ContainerProperty) {
                    return PropertyType.Container;
                } else if (this is IntegerProperty) {
                    return PropertyType.Integer;
                } else if (this is RealProperty) {
                    return PropertyType.Real;
                } else if (this is EnumProperty) {
                    return PropertyType.Enum;
                } else {
                    return PropertyType.NULL;
                }
            }
        }
        public Property() {
            name = "New Property";
            visible = false;
            description = "";
        }
        public Property(string name, bool visible, string description) {
            this.name = name;
            this.visible = visible;
            this.description = description;
        }
        public Property(Property copySource) {
            name = copySource.name;
            visible = copySource.visible;
            description = copySource.description;
        }
    }
    public class ContainerProperty : Property {
        public float? uBound;
        public float defaultValue;
        public string resource;
        public ContainerProperty() : base() {
            uBound = null;
            resource = "";
            defaultValue = 0;
        }
        public ContainerProperty(string name, bool visible, string description, float? uBound, string resource) : base(name, visible, description) {
            this.uBound = uBound;
            this.resource = resource;
        }
        public ContainerProperty(ContainerProperty copySource) : base(copySource) {
            uBound = copySource.uBound;
            resource = copySource.resource;
        }
        public ContainerProperty(Property copySource) : base(copySource) { }
    }
    public class IntegerProperty : Property {
        public int? uBound, lBound;
        public int defaultValue;
        public IntegerProperty() : base() {
            uBound = null;
            lBound = null;
            defaultValue = 0;
        }
        public IntegerProperty(string name, bool visible, string description, int? lBound, int? uBound, int defaultValue) : base(name, visible, description) {
            this.lBound = lBound;
            this.uBound = uBound;
            this.defaultValue = defaultValue;
        }
        public IntegerProperty(IntegerProperty copySource) : base(copySource) {
            lBound = copySource.lBound;
            uBound = copySource.uBound;
            defaultValue = copySource.defaultValue;
        }
        public IntegerProperty(Property copySource) : base(copySource) { }
    }
    public class RealProperty : Property {
        public float? uBound, lBound;
        public float defaultValue;
        public RealProperty() : base() {
            uBound = null;
            lBound = null;
            defaultValue = 0;
        }
        public RealProperty(string name, bool visible, string description, float? lBound, float? uBound, float defaultValue) : base(name, visible, description) {
            this.lBound = lBound;
            this.uBound = uBound;
            this.defaultValue = defaultValue;
        }
        public RealProperty(RealProperty copySource) : base(copySource) {
            lBound = copySource.lBound;
            uBound = copySource.uBound;
            this.defaultValue = copySource.defaultValue;
        }
        public RealProperty(Property copySource) : base(copySource) { }
    }
    public class EnumProperty : Property {
        public List<EnumPropertyValue> enums;
        public int defaultValue;
        public EnumProperty() : base() {
            enums = new List<EnumPropertyValue>();
            defaultValue = 0;
        }
        public EnumProperty(string name, bool visible, string description, List<EnumPropertyValue> enums, int defaultValue) : base(name, visible, description) {
            this.enums = enums;
            this.defaultValue = defaultValue;
        }
        public EnumProperty(EnumProperty copySource) : base(copySource) {
            enums = copySource.enums;
            defaultValue = copySource.defaultValue;
        }
        public EnumProperty(Property copySource) : base(copySource) {
            enums = new List<EnumPropertyValue>();
            defaultValue = 0;
        }
    }
}
