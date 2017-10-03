using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public abstract class Character : MonoBehaviour {

    [Range(0.0f, 10.0f)]
    [SerializeField]
    private float MoveSpeed = 5.0f;

    [SerializeField]
    [Tooltip("Use the direction of the main camera as forward when moving")]
    private bool UseCameraDir = true;

    private CharacterController mCharacterController;


    //-------------------------------------------Unity Functions-------------------------------------------

    private void Start()
    {
        // Getting the 'Character Controller' component.
        mCharacterController = this.GetComponent<CharacterController>();
    }


    //-----------------------------------------Protected Functions-----------------------------------------

    protected void Move(Vector2 moveDir, float speedMultiplier = 1.0f)
    {
        // Aligning to the camera direction if required.
        if (UseCameraDir) AlignToCamera();

        // Calculating the velocity of the character.
        var moveVelocity = new Vector3(moveDir.x, 0.0f, moveDir.y) * MoveSpeed * Mathf.Clamp01(speedMultiplier);
        moveVelocity = this.transform.TransformDirection(moveVelocity);

        // Moving the character controller.
        mCharacterController.Move(moveVelocity * Time.deltaTime);
    }

    protected void ApplyGravity()
    {
        // Applying gravity to the character controller.
        mCharacterController.Move(new Vector3(0.0f, Physics.gravity.y, 0.0f) * Time.deltaTime);
    }


    //------------------------------------------Private Functions------------------------------------------

    private void AlignToCamera()
    {
        // Rotating the character to align with the current direction of the main camera.
        if (Camera.main == null) return;
        var camForwardVector = Camera.main.transform.forward.normalized;
        var targetPos = this.transform.position + camForwardVector;
        var targetRot = Quaternion.LookRotation(new Vector3(targetPos.x, this.transform.position.y, targetPos.z) - this.transform.position);
        this.transform.rotation = targetRot;
    }
}