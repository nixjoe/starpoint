using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

public class ResourceManager : MonoBehaviour {
    public static ResourceManager instance;
    static string dataPath;
    public List<SRL> libraries;

	void Start () {
        if (instance == null) {
            dataPath = Path.Combine(Application.dataPath, "data");
            DontDestroyOnLoad(gameObject);
            instance = this;
            libraries = new List<SRL>();
            LoadSrls(dataPath);
            Debug.Log("Finished loading resource libraries.");
        } else {
            Debug.LogError("Trying to instantiate more than one ResourceManager! Sad!");
        }
    }

    private void LoadSrls(string path) {
        foreach (string fse in Directory.GetFileSystemEntries(path)) {
            if (Directory.Exists(fse)) {
                LoadSrls(fse);
            } else {
                if (Path.GetExtension(fse) == ".srl") {
                    try {
                        string f = Path.GetFileName(fse);
                        string b = f.Split('.')[0];
                        string l = f.Split('.')[1];
                        if (!libraries.Exists(s => s.bundle == b && s.name == l)) {
                            Debug.Log("Loading " + f);
                            SRL srl = SRL.Load(fse);
                            Debug.Log(fse + " version " + srl.version);
                            libraries.Add(srl);
                        } else {
                            Debug.LogError("An SRL with the name " + f + " has already been loaded! >:(");
                        }
                    } catch (Exception e) {
                        Debug.LogError("Failed to load " + Path.GetFileName(fse));
                        Debug.LogError(e.Message);
                        if (e.InnerException != null) {
                            Debug.LogError(e.InnerException.Message);
                        }
                    }
                }
            }
        }
    }

    public StarpointResource Find(string name) {
        string bundle = name.Split('.')[0];
        string library = name.Split('.')[1];
        string resource = name.Split('.')[2];
        if (libraries.Exists(s => s.bundle == bundle && s.name == library)) {
            SRL srl = libraries.Find(s => s.bundle == bundle && s.name == library);
            if (srl.resources.Exists(r => r.name == resource)) {
                return srl.resources.Find(r => r.name == resource);
            } else {
                Debug.LogError(bundle + "." + library + " doesn't contain a resource by that name! Offending resource: " + name);
            }
        } else {
            Debug.LogError("Couldn't find <bundle>.<library> " + bundle + "." + library + " in loaded SRLs! Offending resource: " + name);
        }
        return null;
    }
}
