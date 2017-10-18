using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Player : Character {

    [SerializeField]
    [Tooltip("Use the direction of the main camera as forward when moving.")]
    private bool UseCameraDir = true;

    [SerializeField]
    [Range(10.0f, 30.0f)]
    [Tooltip("The speed at which the player will rotate to align with the camera.")]
    private float RotateSpeed = 20.0f;


    //-------------------------------------------Unity Functions-------------------------------------------

    private void FixedUpdate()
    {
        // Checking for user input.
        var playerMovement = new Vector2(Input.GetAxis("Horizontal"),
                                         Input.GetAxis("Vertical"));

        // Moving the player if an input was provided.
        //if (playerMovement.magnitude > 0.2f)
            Move(playerMovement);

        // Making the player jump if the jump button is pressed.
        if (Input.GetButtonDown("Jump"))
            Jump();
    }


    //-----------------------------------------Protected Functions-----------------------------------------

    protected sealed override void Move(Vector2 moveDir, Transform movementSpaceTrans = null)
    {
        // Aligning the player to the desired direction.
        if (UseCameraDir)
        {
            AlignToCamera();
            movementSpaceTrans = this.transform;
        }
        else
        {
            AlignToVelocity();
            movementSpaceTrans = Camera.main.transform;
        }

        // Calling the base version of the 'Move' function.
        base.Move(moveDir, movementSpaceTrans);
    }


    //------------------------------------------Private Functions------------------------------------------

    private void AlignToCamera()
    {
        // Rotating the player to align with the current direction of the main camera.
        if (Camera.main == null) return;
        var camForwardVector = Camera.main.transform.forward.normalized;
        var targetPos = this.transform.position + camForwardVector;
        var targetRot = Quaternion.LookRotation(new Vector3(targetPos.x, this.transform.position.y, targetPos.z) - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRot, Time.deltaTime * RotateSpeed);
    }

    private void AlignToVelocity()
    {
        // Rotating the player to align with its current velocity.
        var targetPos = transform.position + pVelocity.normalized;
        var targetVector = new Vector3(targetPos.x, this.transform.position.y, targetPos.z) - this.transform.position;
        if (targetVector.magnitude <= 0.1f) return;
        var targetRot = Quaternion.LookRotation(targetVector);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRot, Time.deltaTime * RotateSpeed);
    }
}
