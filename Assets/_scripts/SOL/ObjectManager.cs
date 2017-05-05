using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;

public class ObjectManager : MonoBehaviour {
    public static ObjectManager instance;
    static string dataPath;
    public List<SOL> libraries;
    public List<StarpointModel> models;
    void Start() {
        if (instance == null) {
            dataPath = Path.Combine(Application.dataPath, "data");
            DontDestroyOnLoad(gameObject);
            instance = this;
            libraries = new List<SOL>();
            LoadSols(dataPath);
            models = LoadModels(dataPath);
            Debug.Log("Finished loading object libraries.");
        } else {
            Debug.LogError("Trying to instantiate more than one ObjectManager! Sad!");
        }
    }

    private void LoadSols(string path) {
        foreach (string fse in Directory.GetFileSystemEntries(path)) {
            if (Directory.Exists(fse)) {
                LoadSols(fse);
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
    private List<StarpointModel> LoadModels(string path) {
        List<StarpointModel> models = new List<StarpointModel>();
        foreach (string fse in Directory.GetFileSystemEntries(path)) {
            if (Directory.Exists(fse)) {
                LoadModels(fse);
            } else {
                switch (Path.GetExtension(fse)) {
                    case ".obj":
                        //load obj to mesh
                        models.Add(new StarpointModel(Path.GetFileNameWithoutExtension(fse)));
                        break;
                }
            }
        }
        Debug.Log("breakpoint");
        return models;
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
    public GameObject Instantiate(StarpointObject starpointObject) {
        GameObject go = new GameObject(starpointObject.name);
        StarpointObjectBehaviour starpointObjectBehaviour = go.AddComponent<StarpointObjectBehaviour>();
        starpointObjectBehaviour.starpointObject = new StarpointObject(starpointObject);
        starpointObjectBehaviour.Initialize();
        return go;
    }
}
