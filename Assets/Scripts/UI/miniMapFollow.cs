using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniMapFollow : MonoBehaviour {

    private Vector3 offset;
    public GameObject Tia;
    public int minimapHeight = 10;

	// Use this for initialization
	void Start ()
    {
        InitialPosition();
        offset = transform.position - Tia.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Tia.transform.position + offset;

    }

    public void InitialPosition()
    {
        transform.position = new Vector3(Tia.transform.position.x, Tia.transform.position.y + minimapHeight, Tia.transform.position.z);
    }
}
