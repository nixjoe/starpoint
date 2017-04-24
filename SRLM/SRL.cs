using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace SRLM {
    public class SRL {
        public string name { get; set; }
        public string bundleName { get; set; }
        public string version { get; set; }
        public List<StarpointResource> resourceList;
        public SRL() {
            name = "";
            bundleName = "";
            version = "01.00.00";
            resourceList = new List<StarpointResource>();
        }

        public SRL(string name, string bundleName, string version) {
            this.name = name;
            this.bundleName = bundleName;
            this.version = version;
            resourceList = new List<StarpointResource>();
        }
        public void AddResource(StarpointResource resource) {
            resourceList.Add(resource);
        }
        public static SRL Load(string filepath) {
            if (File.Exists(filepath)) {
                FileStream fs = new FileStream(filepath, FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(SRL));
                return serializer.Deserialize(fs) as SRL;
            } else {
                throw new Exception(filepath + " doesn't exist!");
            }
        }
        public void Save(string destination = "") {
            if (destination == "") {
                destination = Environment.CurrentDirectory + @"\Created Libraries";
            }
            Directory.CreateDirectory(destination);
            XmlSerializer serializer = new XmlSerializer(typeof(SRL));
            string savePath = Path.Combine(destination, string.Join(".", bundleName, name, "srl"));
            if (File.Exists(savePath)) {
                File.Delete(savePath);
            }
            FileStream fs = new FileStream(savePath, FileMode.Create);
            serializer.Serialize(fs, this);
            fs.Close();
        }
    }
}
