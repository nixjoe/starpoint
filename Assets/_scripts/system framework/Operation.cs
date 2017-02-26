using UnityEngine;
using System.Collections.Generic;
using System;
/// <summary>
/// Operations are the "functions" of Gadgets. Examples include a thruster's "thrust" or a gun's "shoot"
/// </summary>
public abstract class Operation {
    public Gadget gadget { get; protected set; }
    public enum TriggerMode { Passive, Semiautomatic, Automatic }
    public string name { get; protected set; }
    public TriggerMode triggerMode { get; protected set; }
    protected List<Effect> _effects;
    public List<Effect> effects { get { return new List<Effect>(_effects); } }
    public Operation(string name) {
        this.name = name;
    }
    /// <summary>
    /// Add a new effect to the Operation. to be done at SOL load.
    /// </summary>
    public void AddEffect(Effect effect) {
        _effects.Add(effect);
    }
    /// <summary>
    /// Perform any normal state updates, primarily cooldown
    /// </summary>
    public virtual void Update() { /*do nothing by default*/ }
    /// <summary>
    /// Activate the operation, if requirements are met.
    /// </summary>
    public virtual void Activate() {
        if (CanActivate()) {
            foreach (Effect effect in _effects) {
                effect.Activate();
            }
        }
    }
    /// <summary>
    /// Check if this operation can be activated
    /// </summary>
    public virtual bool CanActivate() {
        foreach (Effect effect in _effects) {
            if (effect is ResourceSubtractEffect) {
                if (!effect.CanActivate()) {
                    return false;
                }
            }
        }
        return true;
    }
}

public class DiscreteOperation : Operation {
    public float cooldownTime { get; private set; }
    private float cooldownRemaining;

    public DiscreteOperation(string name, float cooldownTime, TriggerMode triggerMode, List<Effect> effects = null) : base(name) {
        this.cooldownTime = cooldownTime;
        cooldownRemaining = 0f;
        _effects = effects;
        this.triggerMode = triggerMode;
    }
    public override void Update() {
        if (cooldownRemaining > 0) {
            cooldownRemaining -= Mathf.Max(0f, cooldownRemaining - Time.deltaTime);
        }
        if (triggerMode == TriggerMode.Passive && CanActivate()) {
            Activate();
        }
    }
    public override bool CanActivate() {
        if (cooldownRemaining == 0) {
            return base.CanActivate();
        } else {
            return false;
        }
    }
}

public class ContinuousOperation : Operation {
    public ContinuousOperation(string name, TriggerMode triggerMode, List<Effect> effects = null) : base(name) {
        if (triggerMode == TriggerMode.Semiautomatic) {
            throw new Exception("Continuous operations cannot be semiautomatic!");
        }
        this.triggerMode = triggerMode;
        _effects = effects;
    }
}
