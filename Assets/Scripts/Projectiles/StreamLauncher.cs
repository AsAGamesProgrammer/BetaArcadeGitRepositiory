using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamLauncher : MonoBehaviour {

    [SerializeField]
    private Vector3 StreamDirection = Vector3.forward;

    [SerializeField]
    [Range(0.0f, 50.0f)]
    private float MaxDistance = 20.0f;

    private Vector3 mStreamVector;


    //-------------------------------------------Unity Functions-------------------------------------------

    


    //------------------------------------------Private Functions------------------------------------------

    Vector3 CalculateStreamVector()
    {
        RaycastHit hit;
        if(Physics.Raycast(this.transform.position, StreamDirection.normalized, out hit, MaxDistance))
        {
            // 
        }
        return Vector3.zero;
    }
}