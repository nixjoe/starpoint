using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class StarpointModel {
    public Mesh mesh;
    public Material material;

    public StarpointModel(Mesh mesh, Material material) {
        //mesh = Resources.Load<Mesh>(modelFile);
        //material = new Material(Shader.Find("Standard"));
        //material.name = Path.GetFileNameWithoutExtension(modelFile);
        //material.SetTexture("Albedo",Resources.L
        this.mesh = mesh;
        this.material = material;
    }
}
