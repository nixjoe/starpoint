using UnityEngine;
using System.Collections;

[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener {
    public override void ControlOfEntityGained(BoltEntity entity) {
        if (entity.gameObject.GetComponent<Camera>() != null) {
            entity.gameObject.GetComponent<Camera>().enabled = true;
        }
    }
}
