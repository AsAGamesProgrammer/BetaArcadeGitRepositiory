using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour {

    [SerializeField]
    private GameObject ProjectilePrefab;

    [SerializeField]
    private float ProjectileSpeed = 1.0f;

    [SerializeField]
    [Range(0.1f, 10.0f)]
    private float TimeBetweenShots = 2.0f;

    [SerializeField]
    private Vector3 ProjectileDirection = Vector3.forward;

    [SerializeField]
    private Vector3 ProjectileSpawnOffset = Vector3.zero;

    private float mTimeOfLastShot = 0.0f;

    private Vector3 pProjectileDirection { get { return this.transform.TransformDirection(ProjectileDirection).normalized; } }
    private Vector3 pProjectileSpawn { get { return this.transform.position + this.transform.TransformDirection(ProjectileSpawnOffset); } }


    //-------------------------------------------Unity Functions-------------------------------------------

    private void OnEnable()
    {
        mTimeOfLastShot = Time.time;
    }

    private void Update()
    {
        if (Time.time - mTimeOfLastShot >= TimeBetweenShots)
            Shoot();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(pProjectileSpawn, 0.25f);
        Gizmos.DrawLine(pProjectileSpawn, pProjectileSpawn + pProjectileDirection.normalized);
    }


    //------------------------------------------Private Functions------------------------------------------

    private void Shoot()
    {
        var obj = Instantiate(ProjectilePrefab, pProjectileSpawn, ProjectilePrefab.transform.rotation) as GameObject;
        obj.transform.LookAt(obj.transform.position + pProjectileDirection);
        obj.GetComponent<Projectile>().Speed = ProjectileSpeed;
        mTimeOfLastShot = Time.time;
    }
}
