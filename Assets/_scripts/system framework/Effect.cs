using UnityEngine;
using System.Collections;
using System;

public abstract class Effect {
    public string name { get; protected set; }
    public Effect(string name) {
        this.name = name;
    }
    public abstract void Activate();
    /// <summary>
    /// All Effects can activate by default.
    /// </summary>
    public virtual bool CanActivate() {
        return true;
    }
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
    /// <summary>
    /// Returns true if there are enough resources to activate.
    /// </summary>
    public override bool CanActivate() {
        throw new NotImplementedException();
    }
    public override void Activate() {
        throw new NotImplementedException();
    }
}
