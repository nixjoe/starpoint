using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

public class SRL {
    public string name { get; set; }
    public string bundle { get; set; }
    public string version { get; set; }

    public List<StarpointResource> resources;
    public SRL() {
        name = "";
        bundle = "";
        version = "";
        resources = new List<StarpointResource>();
    }

    public SRL(string name, string bundleName, string version) {
        this.name = name;
        this.bundle = bundleName;
        this.version = version;
        resources = new List<StarpointResource>();
    }
    public void AddResource(StarpointResource resource) {
        resources.Add(resource);
    }
    public static SRL Load(string filepath) {
        if (File.Exists(filepath)) {
            FileStream fs = new FileStream(filepath, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(SRL));
            SRL srl = serializer.Deserialize(fs) as SRL;
            foreach (StarpointResource r in srl.resources) {
                r.srl = srl;
            }
            return srl;
        } else {
            throw new Exception(filepath + " doesn't exist!");
        }
    }
}
