using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Player : Character {

    [HideInInspector]
    public bool IsDoneDying = false;

    [SerializeField]
    [Tooltip("Use the direction of the main camera as forward when moving.")]
    private bool UseCameraDir = true;

    [SerializeField]
    [Range(10.0f, 30.0f)]
    [Tooltip("The speed at which the player will rotate to align with the camera.")]
    private float RotateSpeed = 20.0f;

    [SerializeField]
    private Animator PlayerAnimator;

    [SerializeField]
    private bool IgnoreInput = false;


    //-------------------------------------------Unity Functions-------------------------------------------

    private void Update()
    {
        // Updating the animator 'IsGrounded' parameter.
        PlayerAnimator.SetBool("IsGrounded", IsGrounded());

        // Checking whether to bother getting player input.
        if (IgnoreInput) return;

        // Checking for user input.
        var playerMovement = new Vector2(Input.GetAxis("Horizontal"),
                                         Input.GetAxis("Vertical"));

        // Moving the player if an input was provided.
        Move(playerMovement);

        // Making the player jump if the jump button is pressed.
        if (Input.GetButtonDown("Jump"))
            Jump();

        // Reseting the push pull animation bool.
        PlayerAnimator.SetBool("PushPullActive", false);

        // Updating the animator 'DidJump' parameter.
        PlayerAnimator.SetBool("DidJump", Input.GetButtonDown("Jump"));        
    }

    private void LateUpdate()
    {
        // Updating the animator 'Speed' parameter.
        var finalVelocity = pVelocity;
        finalVelocity.y = 0.0f;
        PlayerAnimator.SetFloat("AbsSpeed", finalVelocity.magnitude);
        PlayerAnimator.SetFloat("Speed", finalVelocity.magnitude * Mathf.Sign(Input.GetAxis("Vertical")));
    }


    //-------------------------------------------Public Functions------------------------------------------

    public void ToggleIgnoreInput()
    {
        // Toggling the value of 'IgnoreInput'.
        SetIgnoreInput(!IgnoreInput);
    }

    public void SetIgnoreInput(bool value)
    {
        // Setting the value of 'IgnoreInput'.
        IgnoreInput = value;
    }

    public void AddKnockForce(Vector3 force)
    {
        this.GetComponent<Rigidbody>().AddForce(force);
        IgnoreInput = true;
        StartCoroutine(DelayPlayerInput());
    }

    public void Die()
    {
        PlayerAnimator.SetBool("IsDead", true);
        this.GetComponent<Collider>().enabled = false;
        mRigidbody.isKinematic = true;
        IgnoreInput = true;
    }   

    public void Respawn()
    {
        IgnoreInput = false;
        PlayerAnimator.SetBool("IsDead", false);
        this.GetComponent<Collider>().enabled = true;
        mRigidbody.isKinematic = false;
    }

    public void SetPushPullSpeed(float speed)
    {
        PlayerAnimator.SetBool("PushPullActive", true);
        PlayerAnimator.SetFloat("PushPullSpeed", speed);
    }


    //-----------------------------------------Protected Functions-----------------------------------------

    protected sealed override void Move(Vector2 moveDir, float velocityYAngle = 0f)
    {
        // Aligning the player to the camera direction if necessary.
        if (UseCameraDir && moveDir.magnitude > Mathf.Epsilon)
            AlignToCamera();

        // Setting the move direction.
        velocityYAngle = (UseCameraDir) ? this.transform.eulerAngles.y : Camera.main.transform.eulerAngles.y;

        // Calling the base version of the 'Move' function.
        base.Move(moveDir, velocityYAngle);

        // Aligning the player to its velocity if necessary.
        if(!UseCameraDir)
            AlignToVelocity();
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

    private IEnumerator DelayPlayerInput()
    {
        yield return new WaitForSeconds(0.5f);
        while (!IsGrounded())
            yield return new WaitForSeconds(0.1f);
        IgnoreInput = false;
    }
}
