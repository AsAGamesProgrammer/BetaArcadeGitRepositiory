using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class BreakablePlatform : MonoBehaviour {

    [SerializeField]
    [Tooltip("The amount of time in seconds from when the player lands on the platform to when it falls")]
    private float LifeTime = 1.0f;

    private Rigidbody mRigidBody;


    //-------------------------------------------Unity Functions-------------------------------------------

    private void Start()
    {
        mRigidBody = this.GetComponent<Rigidbody>();
        mRigidBody.isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Player>())
            StartSelfDestructTimer();
    }


    //------------------------------------------Private Functions------------------------------------------

    private void StartSelfDestructTimer()
    {
        StartCoroutine(SelfDestructTimer());
    }

    private IEnumerator SelfDestructTimer()
    {
        LifeTime = Mathf.Max(0.0f, LifeTime);
        yield return new WaitForSeconds(LifeTime);
        Break();
    }

    private void Break()
    {
        mRigidBody.isKinematic = false;
        mRigidBody.useGravity = true;
    }
}
