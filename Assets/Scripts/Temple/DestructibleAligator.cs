using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleAligator : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        print("Collision: " + collision.collider.name);
        if (collision.collider.GetComponent<Block>())
        {
            print("Destroy");
            Destroy(gameObject);
        }
    }
}
