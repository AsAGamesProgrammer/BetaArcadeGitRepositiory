using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public abstract class Character : MonoBehaviour {

    [SerializeField]
    [Range(200.0f, 400.0f)]
    private float MoveSpeed = 5.0f;

    private Rigidbody mRigidbody;


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


    //-----------------------------------------Protected Functions-----------------------------------------

    protected virtual void Move(Vector2 moveDir, float speedMultiplier = 1.0f)
    {
        // Calculating the new velocity of the character.
        var currentVelocity = mRigidbody.velocity;
        var newVelocity = new Vector3(moveDir.x, 0.0f, moveDir.y) * MoveSpeed * Mathf.Clamp01(speedMultiplier) * Time.deltaTime;
        newVelocity = this.transform.TransformDirection(newVelocity);
        newVelocity.y = currentVelocity.y;

        // Applying the new velocity to the rigidbody.
        mRigidbody.velocity = newVelocity;
    }
}