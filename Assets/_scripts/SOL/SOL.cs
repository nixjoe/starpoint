using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SOL {
    public List<StarpointObject> objects;
    public string name { get; set; }
    public string bundle { get; set; }
    public string version { get; set; }
    public string fqName {
        get {
            return bundle + "." + name + ".sol";
        }
    }
    public SOL() {
        objects = new List<StarpointObject>();
        version = "01.00.00";
    }
}