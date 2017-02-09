using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[BoltGlobalBehaviour(BoltNetworkModes.Host)]
public class ServerNetworkCallbacks : Bolt.GlobalEventListener {

    public Dictionary<BoltEntity, BoltConnection> playerList;
    public BoltEntity PlayerLookup(BoltConnection connection) {
        foreach (BoltEntity key in playerList.Keys) {
            if (playerList[key] = connection) {
                return key;
            }
        }
        return null;
    }
    public override void BoltStartDone() {
        playerList = new Dictionary<BoltEntity, BoltConnection>();
    }

    public override void Connected(BoltConnection connection) {
        Debug.Log("New connection from " + connection.RemoteEndPoint.ToString());
        InstantiatePlayer(connection);
    }
    public override void Disconnected(BoltConnection connection) {
        BoltNetwork.Destroy(PlayerLookup(connection));
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
