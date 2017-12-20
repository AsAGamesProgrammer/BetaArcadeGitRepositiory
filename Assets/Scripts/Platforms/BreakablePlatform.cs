using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class BreakablePlatform : MonoBehaviour {

    [SerializeField]
    [Tooltip("The amount of time in seconds from when the player lands on the platform to when it falls")]
    private float LifeTime = 1.0f;
    [SerializeField]
    private Vector3 MaxAngularVelOnBreak = Vector3.one;
    [SerializeField]
    private List<Mesh> BrokenMeshes = new List<Mesh>();
    [SerializeField]
    private List<Material> BrokenMaterials = new List<Material>();

    private Rigidbody mRigidBody;
    private bool mHasBeenBroken = false;


    //-------------------------------------------Unity Functions-------------------------------------------

    private void Start()
    {
        mRigidBody = this.GetComponent<Rigidbody>();
        mRigidBody.isKinematic = true;
        //transform.Rotate(Vector3.up, 90f, Space.Self);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Player>() && !mHasBeenBroken)
        {
            StartSelfDestructTimer();

            int minListCount = Mathf.Min(BrokenMaterials.Count, BrokenMeshes.Count);
            if (minListCount > 0)
            {
                int index = Random.Range(0, minListCount);
                GetComponent<MeshFilter>().mesh = BrokenMeshes[index];
                GetComponent<MeshRenderer>().material = BrokenMaterials[index];
            }

            mHasBeenBroken = true;
        }
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
        mRigidBody.angularVelocity = new Vector3(Random.Range(-MaxAngularVelOnBreak.x, MaxAngularVelOnBreak.x),
                                                 Random.Range(-MaxAngularVelOnBreak.y, MaxAngularVelOnBreak.y), 
                                                 Random.Range(-MaxAngularVelOnBreak.z, MaxAngularVelOnBreak.z));
        GetComponent<Collider>().enabled = false;
    }
}
