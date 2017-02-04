using UnityEngine;
using System.Collections;

public class Menu : Bolt.GlobalEventListener {

    string ip;
    void Start() {
        ip = "68.3.18.92";
    }

    void OnGUI() {
        GUI.Label(new Rect(20, 0, 400, 30), "IP address of game to join(make sure port 27550 is open)");
        ip = GUI.TextField(new Rect((Screen.width - 100) / 2, 0, 100, 30), ip);

        GUILayout.BeginArea(new Rect(10, 30, Screen.width - 20, Screen.height - 60));

        if (GUILayout.Button("Create Game", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true))) {
            // START SERVER
            BoltLauncher.StartServer(UdpKit.UdpEndPoint.Parse("0.0.0.0:27550"));
            //BoltNetwork.LoadScene("testWorld");
        }

        if (GUILayout.Button("Join Game", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true))) {
            // START CLIENT
            BoltLauncher.StartClient();
        }
        GUILayout.EndArea();
    }
    public override void BoltStartDone() {
        if (BoltNetwork.isServer) {
            BoltNetwork.LoadScene("testWorld");
        } else {
            BoltNetwork.Connect(UdpKit.UdpEndPoint.Parse(ip + ":27550"));
        }
    }
}