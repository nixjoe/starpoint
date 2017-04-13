using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SOLM {
    [XmlInclude(typeof(PropertyEffect)), XmlInclude(typeof(ResourceEffect)), XmlInclude(typeof(PhysicalEffect)), XmlInclude(typeof(AudioEffect)), XmlInclude(typeof(VisualEffect)), XmlInclude(typeof(ObjectEffect))]
    public class Effect {
        public enum EffectType {NULL, Property, Resource, Physical, Audio, Visual, Object }
        public enum AssignmentType { Equals, Additive, Subtractive, Multiplicative }
        public enum PhysicalType { Force, Torque }
        public enum AudioMode { PlayOnce, Loop }
        public EffectType type {
            get {
                if (this is PropertyEffect) {
                    return EffectType.Property;
                } else if (this is ResourceEffect) {
                    return EffectType.Resource;
                } else if (this is PhysicalEffect) {
                    return EffectType.Physical;
                } else if (this is AudioEffect) {
                    return EffectType.Audio;
                } else if (this is VisualEffect) {
                    return EffectType.Visual;
                } else if (this is ObjectEffect) {
                    return EffectType.Object;
                }
                return EffectType.NULL;
            }
        }
        public Effect() { }
        public Effect(Effect copySource) { }
    }
    public class PropertyEffect : Effect {
        public string property { get; set; }
        public AssignmentType assignmentType { get; set; }
        public string value { get; set; }
        public PropertyEffect() : base() {
            property = "";
            assignmentType = AssignmentType.Equals;
            value = "";
        }
        public PropertyEffect(Effect copySource) : base(copySource) {
            property = "";
            assignmentType = AssignmentType.Equals;
            value = "";
        }
        public PropertyEffect(PropertyEffect copySource) : base(copySource) {
            property = copySource.property;
            assignmentType = copySource.assignmentType;
            value = copySource.value;
        }
    }
    public class ResourceEffect : Effect {
        public string resource { get; set; }
        public AssignmentType assignmentType { get; set; }
        public string value { get; set; }
        public ResourceEffect() : base() {
            resource = "";
            assignmentType = AssignmentType.Equals;
            value = "";
        }
        public ResourceEffect(Effect copySource) : base(copySource) {
            resource = "";
            assignmentType = AssignmentType.Equals;
            value = "";
        }
        public ResourceEffect(ResourceEffect copySource) : base(copySource) {
            resource = copySource.resource;
            assignmentType = copySource.assignmentType;
            value = copySource.value;
        }
    }
    public class PhysicalEffect : Effect {
        public string xPos { get; set; }
        public string yPos { get; set; }
        public string zPos { get; set; }
        public string xValue { get; set; }
        public string yValue { get; set; }
        public string zValue { get; set; }
        public PhysicalType physicalType { get; set; }
        public PhysicalEffect() : base() {
            xPos = "0";
            yPos = "0";
            zPos = "0";
            xValue = "0";
            yValue = "0";
            zValue = "0";
            physicalType = PhysicalType.Force;
        }
        public PhysicalEffect(Effect copySource) : base(copySource) {
            xPos = "0";
            yPos = "0";
            zPos = "0";
            xValue = "0";
            yValue = "0";
            zValue = "0";
            physicalType = PhysicalType.Force;
        }
        public PhysicalEffect(PhysicalEffect copySource) : base(copySource) {
            xPos = copySource.xPos;
            yPos = copySource.yPos;
            zPos = copySource.zPos;
            xValue = copySource.xValue;
            yValue = copySource.yValue;
            zValue = copySource.zValue;
            physicalType = copySource.physicalType;
        }
    }
    public class AudioEffect : Effect {
        public AudioMode audioMode { get; set; }
        public string audioClip { get; set; }
        public AudioEffect() : base() {
            audioMode = AudioMode.PlayOnce;
            audioClip = "";
        }
        public AudioEffect(Effect copySource) : base(copySource) {
            audioMode = AudioMode.PlayOnce;
            audioClip = "";
        }
        public AudioEffect(AudioEffect copySource) : base(copySource) {
            audioMode = copySource.audioMode;
            audioClip = copySource.audioClip;
        }
    }
    public class VisualEffect : Effect {
        public string visual { get; set; }
        public string xPos { get; set; }
        public string yPos { get; set; }
        public string zPos { get; set; }
        public string xRot { get; set; }
        public string yRot { get; set; }
        public string zRot { get; set; }
    public VisualEffect() : base() {
            visual = "";
            xPos = "0";
            yPos = "0";
            zPos = "0";
            xRot = "0";
            yRot = "0";
            zRot = "0";
        }
        public VisualEffect(Effect copySource) : base(copySource) {
            visual = "";
            xPos = "0";
            yPos = "0";
            zPos = "0";
            xRot = "0";
            yRot = "0";
            zRot = "0";
        }
        public VisualEffect(VisualEffect copySource) : base(copySource) {
            visual = copySource.visual;
            xPos = copySource.xPos;
            yPos = copySource.yPos;
            zPos = copySource.zPos;
            xRot = copySource.xRot;
            yRot = copySource.yRot;
            zRot = copySource.zRot;
        }
    }
    public class ObjectEffect : Effect {
        public string obj;
        public string xPos { get; set; }
        public string yPos { get; set; }
        public string zPos { get; set; }
        public string xRot { get; set; }
        public string yRot { get; set; }
        public string zRot { get; set; }
        public string xVel { get; set; }
        public string yVel { get; set; }
        public string zVel { get; set; }
        public string xAng { get; set; }
        public string yAng { get; set; }
        public string zAng { get; set; }
        public ObjectEffect() : base() {
            obj = "";
            xPos = "0";
            yPos = "0";
            zPos = "0";
            xRot = "0";
            yRot = "0";
            zRot = "0";
            xVel = "0";
            yVel = "0";
            zVel = "0";
            xAng = "0";
            yAng = "0";
            zAng = "0";
        }
        public ObjectEffect(Effect copySource) : base(copySource) {
            obj = "";
            xPos = "0";
            yPos = "0";
            zPos = "0";
            xRot = "0";
            yRot = "0";
            zRot = "0";
            xVel = "0";
            yVel = "0";
            zVel = "0";
            xAng = "0";
            yAng = "0";
            zAng = "0";
        }
        public ObjectEffect(ObjectEffect copySource) : base(copySource) {
            obj = copySource.obj;
            xPos = copySource.xPos;
            yPos = copySource.yPos;
            zPos = copySource.zPos;
            xRot = copySource.xRot;
            yRot = copySource.yRot;
            zRot = copySource.zRot;
            xVel = copySource.xVel;
            yVel = copySource.yVel;
            zVel = copySource.zVel;
            xAng = copySource.xAng;
            yAng = copySource.yAng;
            zAng = copySource.zAng;
        }
    }


}
