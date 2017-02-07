using UnityEngine;
using System.Collections;
using Bolt;

public class PlayerBehaviour : Bolt.EntityBehaviour<IPlayerState> {
    public InputCluster input;
    public Rigidbody rb;

    public override void Attached() {
        rb = GetComponent<Rigidbody>();
        state.SetTransforms(state.Transform, transform);
    }

    public void Update() {
        input.ReadInput(true);
    }
    public override void SimulateController() {
        input.ReadInput(false);

        IPlayerInputCommandInput commandInput = PlayerInputCommand.Create();
        commandInput.right = input.right;
        commandInput.forward = input.forward;
        commandInput.roll = input.roll;
        commandInput.up = input.up;
        commandInput.pitch = input.pitch;
        commandInput.yaw = input.yaw;

        entity.QueueInput(commandInput);
    }

    public override void ExecuteCommand(Command command, bool resetState) {
        PlayerInputCommand cmd = (PlayerInputCommand)command;
        if (resetState) {
            transform.position = cmd.Result.position;
            transform.rotation = cmd.Result.rotation;
            rb.velocity = cmd.Result.velocity;
            rb.angularVelocity = cmd.Result.angularVelocity;
        } else {
            ProcessInput(cmd);
            cmd.Result.position = transform.position;
            cmd.Result.rotation = transform.rotation;
            cmd.Result.velocity = rb.velocity;
            cmd.Result.angularVelocity = rb.angularVelocity;
        }
    }

    private void ProcessInput(PlayerInputCommand cmd) {
        rb.AddRelativeForce(cmd.Input.right, cmd.Input.up, cmd.Input.forward, ForceMode.Force);
        rb.AddRelativeTorque(cmd.Input.pitch, cmd.Input.yaw, cmd.Input.roll, ForceMode.Acceleration);
    }
}
