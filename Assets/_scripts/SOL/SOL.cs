using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System;

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
    public static SOL Load(string filepath) {
        if (File.Exists(filepath)) {
            FileStream fs = new FileStream(filepath, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(SOL));
            SOL sol = serializer.Deserialize(fs) as SOL;
            foreach (StarpointObject r in sol.objects) {
                r.library = sol;
            }
            return sol;
        } else {
            throw new Exception(filepath + " doesn't exist!");
        }
    }
}