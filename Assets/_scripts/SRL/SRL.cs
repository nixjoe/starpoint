using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

public class SRL {
    public string name { get; set; }
    public string bundleName { get; set; }
    public List<StarpointResource> resourceList;
    public SRL() {
        name = "";
        bundleName = "";
        resourceList = new List<StarpointResource>();
    }

    public SRL(string name, string bundleName) {
        this.name = name;
        this.bundleName = bundleName;
        resourceList = new List<StarpointResource>();
    }
    public void AddResource(StarpointResource resource) {
        resourceList.Add(resource);
    }
    public static SRL Load(string filepath) {
        if (File.Exists(filepath)) {
            FileStream fs = new FileStream(filepath, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(SRL));
            SRL srl = serializer.Deserialize(fs) as SRL;
            foreach (StarpointResource r in srl.resourceList) {
                r.srl = srl;
            }
            return srl;
        } else {
            throw new Exception(filepath + " doesn't exist!");
        }
    }
}
