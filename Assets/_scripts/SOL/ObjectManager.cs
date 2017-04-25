using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

public class ObjectManager : MonoBehaviour {
    public static ObjectManager instance;
    public static string srlPath;
    public List<SOL> libraries;

    void Start() {
        if (instance == null) {
            srlPath = Path.Combine(Application.dataPath, "data");
            DontDestroyOnLoad(gameObject);
            instance = this;
            libraries = new List<SOL>();
            LoadSrls(srlPath);
            Debug.Log("Finished loading object libraries.");
        } else {
            Debug.LogError("Trying to instantiate more than one ObjectManager! Sad!");
        }
    }

    private void LoadSrls(string path) {
        foreach (string fse in Directory.GetFileSystemEntries(path)) {
            if (Directory.Exists(fse)) {
                LoadSrls(fse);
            } else {
                if (Path.GetExtension(fse) == ".sol") {
                    try {
                        string f = Path.GetFileName(fse);
                        string b = f.Split('.')[0];
                        string l = f.Split('.')[1];
                        if (!libraries.Exists(s => s.bundle == b && s.name == l)) {
                            Debug.Log("Loading " + f);
                            SOL sol = SOL.Load(fse);
                            Debug.Log(fse + " version " + sol.version);
                            libraries.Add(sol);
                        } else {
                            Debug.LogError("An SOL with the name " + f + " has already been loaded! >:(");
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

    public StarpointObject Find(string name) {
        string bundle = name.Split('.')[0];
        string library = name.Split('.')[1];
        string obj = name.Split('.')[2];
        if (libraries.Exists(s => s.bundle == bundle && s.name == library)) {
            SOL sol = libraries.Find(s => s.bundle == bundle && s.name == library);
            if (sol.objects.Exists(o => o.name == obj)) {
                return sol.objects.Find(o => o.name == obj);
            } else {
                Debug.LogError(bundle + "." + library + " doesn't contain an object by that name! Offending object: " + name);
            }
        } else {
            Debug.LogError("Couldn't find <bundle>.<library> " + bundle + "." + library + " in loaded SOLs! Offending object: " + name);
        }
        return null;
    }
}
