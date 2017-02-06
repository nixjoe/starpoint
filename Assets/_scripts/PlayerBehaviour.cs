using UnityEngine;
using System.Collections;

public class PlayerBehaviour : Bolt.EntityBehaviour<IPlayerState> {

    public override void Attached() {
        state.SetTransforms(state.PlayerTransform, transform);
    }
}
