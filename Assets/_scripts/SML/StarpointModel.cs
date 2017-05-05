using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

public class StarpointModel {
    public Mesh mesh;
    public Material material;
    string data = Path.Combine(Application.dataPath, "data");
    public StarpointModel(Mesh mesh, Material material) {
        //mesh = Resources.Load<Mesh>(modelFile);
        //material = new Material(Shader.Find("Standard"));
        //material.name = Path.GetFileNameWithoutExtension(modelFile);
        //material.SetTexture("Albedo",Resources.L
        this.mesh = mesh;
        this.material = material;
    }
    /// <summary>
    /// Searches the data folder for 
    /// </summary>
    /// <param name="name"></param>
    public StarpointModel(string name) {
        string albedoTransparency = "";
        string metallicSmoothness = "";
        string normal = "";
        string obj = "";
        List<string> relatedFiles = GetRelatedFiles(data, Path.GetFileNameWithoutExtension(name));
        foreach (string file in relatedFiles) {
            switch (Path.GetExtension(file)) {
                case ".obj":
                    obj = file;
                    break;
                case ".png":
                    if (file.IndexOf("AlbedoTransparency") != -1) {
                        albedoTransparency = file;
                    }
                    if (file.IndexOf("MetallicSmoothness") != -1) {
                        metallicSmoothness = file;
                    }
                    if (file.IndexOf("Normal") != -1) {
                        normal = file;
                    }
                    break;
            }
        }
        GameObject go = OBJLoader.LoadOBJFile(obj);
        mesh = go.GetComponentInChildren<MeshFilter>().mesh;
        GameObject.Destroy(go);
        //Mesh m = ReadObjFile(obj);
    }

    private static Mesh ReadObjFile(string obj) {
        if (File.Exists(obj)) {
            List<string> objLines = File.ReadAllLines(obj).ToList();
            List<Vector3> vertices = new List<Vector3>();
            List<int> triangles = new List<int>();
            List<Vector2> loadedUVs = new List<Vector2>();
            List<Vector3> loadedNormals = new List<Vector3>();
            List<Vector2> uvs = new List<Vector2>();
            List<Vector3> normals = new List<Vector3>();
            foreach (string line in objLines) {
                List<string> splitLine = line.Trim().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                switch (splitLine.First()) {
                    case "v":
                        vertices.Add(new Vector3(float.Parse(splitLine[1]), float.Parse(splitLine[2]), float.Parse(splitLine[3])));
                        break;
                    case "vt":
                        loadedUVs.Add(new Vector2(float.Parse(splitLine[1]), float.Parse(splitLine[2])));
                        break;
                    case "vn":
                        loadedNormals.Add(new Vector3(float.Parse(splitLine[1]), float.Parse(splitLine[2]), float.Parse(splitLine[3])));
                        break;
                    case "f":
                        for (int i = 1; i < 4; i++) {
                            List<string> f = splitLine[i].Split('/').ToList();
                            triangles.Add(int.Parse(f[0])-1);
                            uvs.Add(loadedUVs[int.Parse(f[1])-1]);
                            normals.Add(loadedNormals[int.Parse(f[2])-1]);
                        }
                        break;
                }
            }
            Mesh m = new Mesh();
            m.vertices = vertices.ToArray();
            m.normals = normals.ToArray();
            m.uv = uvs.ToArray();
            m.triangles = triangles.ToArray();
            return m;
        } else {
            throw new FileNotFoundException("Couldn't find a '.obj' file for that object name!", obj);
        }
    }

    List<string> GetRelatedFiles(string path, string name) {
        List<string> relatedFiles = new List<string>();
        foreach (string fse in Directory.GetFileSystemEntries(path)) {
            if (Directory.Exists(fse)) {
                //is directory
                relatedFiles.AddRange(GetRelatedFiles(fse, name));
            } else {
                if (fse.IndexOf(name) != -1) {
                    relatedFiles.Add(fse);
                }
            }
        }
        return relatedFiles;
    }
}
