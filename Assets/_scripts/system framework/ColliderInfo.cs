using UnityEngine;
using System.Collections;
/// <summary>
/// Contains all necessary data to create and configure a Unity Collider.
/// </summary>
public abstract class ColliderInfo {
    public Vector3 center { get; protected set; }
}

public class SphereColliderInfo : ColliderInfo {
    public float radius { get; private set; }
    public SphereColliderInfo(Vector3 center, float radius) {
        this.center = center;
        this.radius = radius;
    }
}

public class BoxColliderInfo : ColliderInfo {
    public Vector3 size { get; private set; }
    public BoxColliderInfo(Vector3 center, Vector3 size) {
        this.center = center;
        this.size = size;
    }
}

public class CapsuleColliderInfo : ColliderInfo {
    public float radius { get; private set; }
    public float height { get; private set; }
    public CapsuleColliderInfo(Vector3 center, float radius, float height) {
        this.center = center;
        this.radius = radius;
        this.height = height;
    }
}
