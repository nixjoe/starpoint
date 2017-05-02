using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

public class ObjectManager : MonoBehaviour {
    public static ObjectManager instance;
    public static string solPath;
    public List<SOL> libraries;
    void Start() {
        if (instance == null) {
            solPath = Path.Combine(Application.dataPath, "Resources");
            DontDestroyOnLoad(gameObject);
            instance = this;
            libraries = new List<SOL>();
            LoadSols(solPath);
            LoadModels(solPath);
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
    private void LoadModels(string path) {
        List<Mesh> meshes = new List<Mesh>();
        List<Texture2D> textures = new List<Texture2D>();
        foreach (string fse in Directory.GetFileSystemEntries(path)) {
            if (Directory.Exists(fse)) {
                LoadModels(fse);
            } else {
                Mesh m;
                Texture2D t;
                switch (Path.GetExtension(fse)) {
                    case ".fbx":
                        //load fbx to mesh
                        m = Resources.Load<Mesh>(fse);
                        m.name = Path.GetFileNameWithoutExtension(fse);
                        meshes.Add(m);
                        break;
                    case ".obj":
                        //load obj to mesh
                        m = Resources.Load<Mesh>(fse);
                        m.name = Path.GetFileNameWithoutExtension(fse);
                        meshes.Add(m);
                        break;
                    case ".png":
                        //load png to texture
                        t = Resources.Load<Texture2D>(fse);
                        t.name = Path.GetFileNameWithoutExtension(fse);
                        textures.Add(t);
                        break;
                }
            }
        }
        Debug.Log("breakpoint");
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
