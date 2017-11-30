using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamCollider : MonoBehaviour {

    [SerializeField]
    private float PushAwayForce = 1.0f;


    //-------------------------------------------Unity Functions-------------------------------------------

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.GetComponent<Player>())
        {
            var pointToPlayer = collision.collider.transform.position - collision.contacts[0].point;
            pointToPlayer += Vector3.up;
            var averageVector = pointToPlayer / 2.0f;

            collision.collider.GetComponent<Player>().AddKnockForce(averageVector.normalized * PushAwayForce);
        }
    }
}
