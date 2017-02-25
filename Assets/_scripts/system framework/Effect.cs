using UnityEngine;
using System.Collections;
using System;

public abstract class Effect {
    public string name { get; protected set; }
    public Effect(string name) {
        this.name = name;
    }
    public abstract void Activate();
}

public class ResourceAddEffect : Effect {
    public ResourceAddEffect(string name) : base(name) {
    }

    public override void Activate() {
        throw new NotImplementedException();
    }
}

public class ResourceSubtractEffect : Effect {
    public ResourceSubtractEffect(string name) : base(name) {
    }
    public bool HasEnoughResources() {
        throw new NotImplementedException();
    }
    public override void Activate() {
        throw new NotImplementedException();
    }
}
