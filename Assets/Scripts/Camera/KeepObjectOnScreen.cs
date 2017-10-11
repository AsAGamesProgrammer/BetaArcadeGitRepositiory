using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepObjectOnScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.2f, 0.8f);
        //pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);
        pos.z = Mathf.Clamp(pos.z, 0.5f, 1.0f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}
