using UnityEngine;
using System.Collections;

public struct InputCluster {
    //public const float MOUSE_SENSITIVITY = 2.0f;

    public float forward { get; private set; }
    //public float backward { get; private set; }
    //public float left { get; private set; }
    public float right { get; private set; }
    //public float rollLeft { get; private set; }
    public float roll { get; private set; }
    public float up { get; private set; }
    //public float down { get; private set; }
    public float pitch { get; private set; }
    public float yaw { get; private set; }
    public bool exit { get; private set; }
    public void ReadInput(bool mouse) {

        forward = Input.GetAxis("Forward/Backward");
        right = Input.GetAxis("Right/Left");
        up = Input.GetAxis("Up/Down");
        roll = Input.GetAxis("Roll");
        exit = Input.GetButton("Exit");
        if (mouse) {
            yaw = (Input.GetAxis("Yaw"));
            //yaw %= 360f;

            pitch = (-Input.GetAxis("Pitch"));
            //pitch %= 360f;
        }
    }
}
