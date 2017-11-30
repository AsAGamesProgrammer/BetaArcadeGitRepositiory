using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public abstract class Character : MonoBehaviour {

    public float pMoveSpeed { get { return MoveSpeed; } }

    protected Vector3 pVelocity { get { return mRigidbody.velocity; } }

    [SerializeField]
    [Range(0.0f, 1000.0f)]
    [Tooltip("The horizontal speed at which the character will move.")]
    private float MoveSpeed = 400.0f;

    [SerializeField]
    [Range(0.0f, 1000.0f)]
    [Tooltip("The vertical force applied to the character when they jump.")]
    private float JumpForce = 400.0f;

    [SerializeField]
    [Tooltip("Allow the character to move while in mid air.")]
    private bool HasAirControl = true;

    [SerializeField]
    [Tooltip("Allow the character to jump once while in mid air.")]
    private bool CanDoubleJump = false;

    protected Rigidbody mRigidbody;
    protected Vector3 mAccurateVelocity = Vector3.zero;

    private bool mHasDoubleJumped = false;


    //-------------------------------------------Unity Functions-------------------------------------------

    private void Start()
    {
        // Getting the rigidbody component.
        mRigidbody = this.GetComponent<Rigidbody>();

        // Setting rigidbody constraints.
        mRigidbody.drag = 0.0f;
        mRigidbody.maxAngularVelocity = 0.0f;
        mRigidbody.useGravity = true;
        mRigidbody.isKinematic = false;
        mRigidbody.interpolation = RigidbodyInterpolation.None;
        mRigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        mRigidbody.constraints = RigidbodyConstraints.FreezeRotationX |
                                 RigidbodyConstraints.FreezeRotationZ;
    }

    protected virtual void Update()
    {
        mAccurateVelocity = pVelocity;
    }

    private void OnCollisionStay(Collision collision)
    {
        // Reseting double jump if the character is grounded.
        if (IsGrounded()) mHasDoubleJumped = false;
    }


    //-----------------------------------------Protected Functions-----------------------------------------

    protected virtual void Move(Vector2 moveDir, Transform movementSpaceTrans)
    {
        // Exiting early if air control is disabled and the character is in the air.
        if (!IsGrounded() && !HasAirControl) return;

        // Calculating the new velocity of the character.
        var currentVelocity = mRigidbody.velocity;
        var newVelocity = (new Vector3(moveDir.x, 0.0f, moveDir.y)).normalized * MoveSpeed * Time.deltaTime;
        newVelocity = movementSpaceTrans.TransformDirection(newVelocity);
        newVelocity.y = currentVelocity.y;

        // Applying the new velocity to the rigidbody.
        mRigidbody.velocity = newVelocity;
    }

    protected virtual void Jump()
    {
        // Checking if the character is grounded.
        if (!IsGrounded())
        {
            // Allowing the character to jump in mid air if double jumping is enabled.
            if (CanDoubleJump && !mHasDoubleJumped) mHasDoubleJumped = true;

            // Exiting early if double jump isn't enabled or the character has already double jumped.
            else return;
        }

        // Removing any vertical velocity that the rigidbody has.
        mRigidbody.velocity = new Vector3(mRigidbody.velocity.x, 0.0f, mRigidbody.velocity.z);

        // Adding the jump force to the rigidbody.
        mRigidbody.AddForce(Vector3.up * JumpForce);
    }

    protected bool IsGrounded()
    {
        // Raycasting down out the bottom of the character.
        RaycastHit hit;
        Physics.Raycast(this.transform.position, Vector3.down, out hit, Mathf.Abs(this.transform.lossyScale.y) + 0.01f);

        // Returning true if the ray hit something.
        return (hit.collider != null);
    }
}