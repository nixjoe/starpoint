using UnityEngine;
using System.Collections.Generic;

[BoltGlobalBehaviour(BoltNetworkModes.Host)]
public class NetworkCallbacks : Bolt.GlobalEventListener {

    public Dictionary<BoltEntity, BoltConnection> playerList;

    public override void BoltStartDone() {
        playerList = new Dictionary<BoltEntity, BoltConnection>();
    }

    public override void Connected(BoltConnection connection) {
        Debug.Log("New connection from " + connection.RemoteEndPoint.ToString());
        InstantiatePlayer(connection);
    }
    public override void SceneLoadLocalDone(string map) {
        //instantiate the host player
        InstantiatePlayer(null);
    }
    public void InstantiatePlayer(BoltConnection connection) {
        Vector3 pos = new Vector3(Random.Range(-16, 16), 0, Random.Range(-16, 16));
        BoltEntity entity = BoltNetwork.Instantiate(BoltPrefabs.Player, pos, Quaternion.identity);
        playerList.Add(entity, connection);
        if (connection == null) {
            entity.TakeControl();
        } else {
            entity.AssignControl(connection);
        }
    }
}
