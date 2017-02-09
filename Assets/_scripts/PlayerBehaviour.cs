using UnityEngine;
using System.Collections;
using Bolt;

public class PlayerBehaviour : Bolt.EntityBehaviour<IPlayerState> {
    public InputCluster input;
    public Rigidbody rb;
    public float movementTorque, movementForce, maxStoppingTorque;
    public Collider initialCollider, actualCollider;
    public override void Attached() {
        //rb = GetComponent<Rigidbody>();
        state.SetTransforms(state.Transform, transform);
        Vector3 it = rb.inertiaTensor;
        actualCollider.enabled = true;
        initialCollider.enabled = false;
        rb.inertiaTensor = it;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Update() {
        input.ReadInput(true);

        //if (Cursor.lockState != CursorLockMode.Locked) {
        //    Cursor.lockState = CursorLockMode.Locked;
        //    Cursor.visible = true;
        //}
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
        commandInput.exit = input.exit;
        entity.QueueInput(commandInput);
    }

    public override void ExecuteCommand(Command command, bool resetState) {
        PlayerInputCommand cmd = (PlayerInputCommand)command;
        if (resetState) {
            transform.position = cmd.Result.position;
            transform.rotation = cmd.Result.rotation;
            rb.velocity = cmd.Result.velocity;
            rb.angularVelocity = cmd.Result.angularVelocity;
            if (cmd.Input.exit) {
                BoltNetwork.Detach(gameObject);
                Application.Quit();
            }
        } else {
            if (cmd.IsFirstExecution) {
                ProcessInput(cmd);
                cmd.Result.position = transform.position;
                cmd.Result.rotation = transform.rotation;
                cmd.Result.velocity = rb.velocity;
                cmd.Result.angularVelocity = rb.angularVelocity;
            }

        }
    }
    public override void Detached() {
        Destroy(gameObject);
    }
    private void ProcessInput(PlayerInputCommand cmd) {
        rb.AddRelativeForce(cmd.Input.right * movementForce, cmd.Input.up * movementForce, cmd.Input.forward * movementForce, ForceMode.Impulse);
        LookUpdate(cmd.Input.roll, cmd.Input.pitch,cmd.Input.yaw);
        //rb.AddRelativeTorque(cmd.Input.pitch, cmd.Input.yaw, cmd.Input.roll, ForceMode.Acceleration);
    }
    private void LookUpdate(float roll, float pitch, float yaw) {
        
        //Debug.Log("Y: " + yaw + ", P: " + pitch + ", R: " + roll);
        if (Mathf.Abs(roll) >= 0.1f) {
            //if (!isBuilding) {
                rb.AddRelativeTorque(0f, 0f, roll * -movementTorque / 10f, ForceMode.Impulse);
            //}
        } else {
            if (transform.InverseTransformDirection(rb.angularVelocity).z < 0) {
                rb.AddRelativeTorque(0f, 0f, Mathf.Clamp(-transform.InverseTransformDirection(rb.angularVelocity).z, 0, maxStoppingTorque), ForceMode.Impulse);
            } else {
                rb.AddRelativeTorque(0f, 0f, Mathf.Clamp(-transform.InverseTransformDirection(rb.angularVelocity).z, -maxStoppingTorque, 0), ForceMode.Impulse);
            }
        }
        float mouseX = yaw;
        float mouseY = pitch;
        //if mouse x isn't moving, stop
        if (Mathf.Abs(mouseX) <= 0.1f) {
            if (transform.InverseTransformDirection(rb.angularVelocity).y < 0) {
                rb.AddRelativeTorque(0f, Mathf.Clamp(-transform.InverseTransformDirection(rb.angularVelocity).y, 0, maxStoppingTorque), 0f, ForceMode.Impulse);
            } else {
                rb.AddRelativeTorque(0f, Mathf.Clamp(-transform.InverseTransformDirection(rb.angularVelocity).y, -maxStoppingTorque, 0), 0f, ForceMode.Impulse);
            }
            //otherwise, apply mouse as torque
        } else {
            if (mouseX > 0) {
                rb.AddRelativeTorque(0f, Mathf.Clamp(mouseX, 0, movementTorque), 0f, ForceMode.Impulse);
            } else {
                rb.AddRelativeTorque(0f, Mathf.Clamp(mouseX, -movementTorque, 0), 0f, ForceMode.Impulse);
            }
        }
        //if mouse y isn't moving, stop
        if (Mathf.Abs(mouseY) <= 0.1f) {
            if (transform.InverseTransformDirection(rb.angularVelocity).x < 0) {
                rb.AddRelativeTorque(Mathf.Clamp(-transform.InverseTransformDirection(rb.angularVelocity).x, 0, maxStoppingTorque), 0f, 0f, ForceMode.Impulse);
            } else {
                rb.AddRelativeTorque(Mathf.Clamp(-transform.InverseTransformDirection(rb.angularVelocity).x, -maxStoppingTorque, 0), 0f, 0f, ForceMode.Impulse);
            }
            //otherwise, apply mouse as torque
        } else {
            if (mouseY > 0) {
                rb.AddRelativeTorque(Mathf.Clamp(mouseY, 0, movementTorque), 0f, 0f, ForceMode.Impulse);
            } else {
                rb.AddRelativeTorque(Mathf.Clamp(mouseY, -movementTorque, 0), 0f, 0f, ForceMode.Impulse);
            }
        }
    }
}
