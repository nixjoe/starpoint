using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

public class StarpointResource {
    public string name { get; set; }
    public float weight { get; set; }
    [XmlIgnore]
    public SRL srl { get; set; }
    public string fqName {
        get {
            return srl.bundleName + '.' + srl.name + '.' + name;
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