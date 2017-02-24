using UnityEngine;
using System.Collections.Generic;

public abstract class Operation {
    public string name { get; protected set; }
    public Operation(string name) {
        this.name = name;
    }
    public abstract void Update();
    public abstract void Activate();
    public abstract bool CanActivate();
}

public abstract class DiscreteOperation : Operation {
    public float cooldownTime { get; private set; }
    private float cooldownRemaining;
    private List<Effect> _effects;
    public List<Effect> effects { get { return new List<Effect>(_effects); } }
    public DiscreteOperation(string name, float cooldownTime, List<Effect> effects = null) : base(name) {
        this.cooldownTime = cooldownTime;
        cooldownRemaining = 0f;
        this._effects = effects;
    }
    public void AddEffect(Effect effect) {
        _effects.Add(effect);
    }
}
