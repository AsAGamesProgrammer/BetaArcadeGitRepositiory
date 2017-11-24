using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniMapFollow : MonoBehaviour {

    private Vector3 offset;
    public GameObject Tia;

	// Use this for initialization
	void Start ()
    {
        offset = transform.position - Tia.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Tia.transform.position + offset;
	}
}
