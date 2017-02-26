using UnityEngine;
using System.Collections;
using System;

public abstract class Effect {
    public Operation operation { get; protected set; }
    public string name { get; protected set; }
    public Effect(string name, Operation operation) {
        this.name = name;
        this.operation = operation;
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
    public ResourceAddEffect(string name, Operation operation) : base(name, operation) {
    }

    public override void Activate() {
        throw new NotImplementedException();
    }
}

public class ResourceSubtractEffect : Effect {
    public ResourceSubtractEffect(string name, Operation operation) : base(name, operation) {
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
public class PhysicalEffect : Effect {
    public Vector3 torque { get; private set; }
    public Vector3 force { get; private set; }

    public PhysicalEffect(string name, Operation operation, Vector3 torque, Vector3 force) : base(name, operation) {
        this.torque = torque;
        this.force = force;
    }

    public override void Activate() {
        throw new NotImplementedException();
    }
}

public class AudioEffect : Effect {
    AudioSource audioSource;
    public AudioEffect(string name, Operation operation, AudioSource audioSource) : base(name, operation) {
        this.audioSource = audioSource;
    }

    public override void Activate() {
        if (!audioSource.isPlaying) {
            audioSource.Play();
        } else {
            Debug.LogWarning("Audio source is trying to start playing but is already playing!");
        }
    }
}

public class VisualEffect : Effect {
    public VisualEffect(string name, Operation operation) : base(name, operation) {
    }

    public override void Activate() {
        throw new NotImplementedException();
    }
}
