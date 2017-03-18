using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLM {
    public abstract class Property {
        public string name;
        public bool visible;
        public string description;
        public Property() {
            name = "NewProperty";
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
        public string resource;
        public ContainerProperty() : base() {
            uBound = null;
            resource = "";
        }
        public ContainerProperty(string name, bool visible, string description, float? uBound, string resource) : base(name, visible, description) {
            this.uBound = uBound;
            this.resource = resource;
        }
        public ContainerProperty(ContainerProperty copySource) : base(copySource) {
            uBound = copySource.uBound;
            resource = copySource.resource;
        }
    }
    public class IntegerProperty : Property {
        public int? uBound, lBound;
        public IntegerProperty() : base() {
            uBound = null;
            lBound = null;
        }
        public IntegerProperty(string name, bool visible, string description, int? lBound, int? uBound) : base(name, visible, description) {
            this.lBound = lBound;
            this.uBound = uBound;
        }
        public IntegerProperty(IntegerProperty copySource) : base(copySource) {
            lBound = copySource.lBound;
            uBound = copySource.uBound;
        }
    }
    public class RealProperty : Property {
        public float? uBound, lBound;
        public RealProperty() : base() {
            uBound = null;
            lBound = null;
        }
        public RealProperty(string name, bool visible, string description, float? lBound, float? uBound) : base(name, visible, description) {
            this.lBound = lBound;
            this.uBound = uBound;
        }
        public RealProperty(RealProperty copySource) : base(copySource) {
            lBound = copySource.lBound;
            uBound = copySource.uBound;
        }
    }
    public class EnumProperty : Property {
        public List<EnumPropertyValue> enums;
        public EnumProperty() : base() {
            enums = new List<EnumPropertyValue>();
        }
        public EnumProperty(string name, bool visible, string description, List<EnumPropertyValue> enums) : base(name, visible, description) {
            this.enums = enums;
        }
        public EnumProperty(EnumProperty copySource) : base(copySource) {
            enums = copySource.enums;
        }
    }
}
