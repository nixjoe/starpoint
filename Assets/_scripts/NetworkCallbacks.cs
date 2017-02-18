using UnityEngine;
using System.Collections;

[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener {
    public override void ControlOfEntityGained(BoltEntity entity) {
        if (entity.gameObject.GetComponent<PlayerBehaviour>().cam != null) {
            entity.gameObject.GetComponent<PlayerBehaviour>().cam.enabled = true;
        }
    }
}
