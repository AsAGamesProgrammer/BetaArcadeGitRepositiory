using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {

    [HideInInspector]
    public float Speed = 1.0f;

    [SerializeField]
    private float Lifetime = 5.0f;

    private float mTimeOfSpawn = 0.0f;


    //-------------------------------------------Unity Functions-------------------------------------------

    private void Start()
    {
        mTimeOfSpawn = Time.time;
        GetComponent<Rigidbody>().velocity = Speed * this.transform.forward;
    }

    private void Update()
    {
        if (Time.time - mTimeOfSpawn >= Lifetime)
            Die();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Die();
    }


    //------------------------------------------Private Functions------------------------------------------

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
