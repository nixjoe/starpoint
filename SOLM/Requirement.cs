using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SOLM {
    [XmlInclude(typeof(ResourceRequirement)), XmlInclude(typeof(PropertyRequirement))]
    public class Requirement {
        public float value { get; set; }
        public enum ComparisonType { Equals, LessThan, GreaterThan, LessThanOrEquals, GreaterThanOrEquals, NotEquals };
        public enum RequirementType { Resource, Property };
        public RequirementType type {
            get {
                if (this is ResourceRequirement) {
                    return RequirementType.Resource;
                } else {
                    return RequirementType.Property;
                }
            }
        }
        public string variable {
            get {
                if (this is ResourceRequirement) {
                    return (this as ResourceRequirement).resource;
                } else {
                    return (this as PropertyRequirement).property;
                }
            }
        }
        public ComparisonType comparison { get; set; }
        public Requirement() {
            value = 0;
            comparison = ComparisonType.Equals;
        }
        public Requirement(Requirement copySource) {
            value = copySource.value;
            comparison = copySource.comparison;
        }
    }
    public class PropertyRequirement : Requirement {
        public string property { get; set; }
        public PropertyRequirement() : base() {
            property = "";
        }
        public PropertyRequirement(Requirement copySource) : base(copySource) {
            property = "";
        }
        public PropertyRequirement(PropertyRequirement copySource) : base(copySource) {
            property = copySource.property;
        }
    }
    public class ResourceRequirement : Requirement {
        public string resource { get; set; }
        public ResourceRequirement() : base() {
            resource = "";
        }
        public ResourceRequirement(Requirement copySource) : base(copySource) {
            resource = "";
        }
        public ResourceRequirement(ResourceRequirement copySource) : base(copySource) {
            resource = copySource.resource;
        }
    }
}
