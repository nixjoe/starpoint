using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Gadget {
    //kg
    public float mass { get; private set; }
    public float condition { get; private set; }
    public float maxCondition { get; private set; }
    public string bundle { get; private set; }
    public string name { get; private set; }
    public string modelFile { get; private set; }
    //J
    public float heat { get; private set; }
    //J
    public float maxHeat { get; private set; }
    public List<ColliderInfo> colliders { get; private set; }

    public Gadget(string bundle, string name, string modelFile, float mass, float condition, float maxHeat, List<ColliderInfo> colliders = null) {
        this.bundle = bundle;
        this.name = name;
        this.modelFile = modelFile;
        this.mass = mass;
        this.condition = condition;
        maxCondition = condition;
        this.maxHeat = maxHeat;
        heat = 0f;
        this.colliders = colliders;
    }
    public void AddCollider(ColliderInfo collider) {
        colliders.Add(collider);
    }
}
