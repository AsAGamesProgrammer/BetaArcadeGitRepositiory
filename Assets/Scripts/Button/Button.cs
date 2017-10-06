using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    enum Dir {Up, Down}

    [SerializeField]
    private string PlayerTag = "Player";

    [SerializeField]
    private float PushSpeed = 1.0f;

    [SerializeField]
    private float ExtendSpeed = 1.0f;

    [SerializeField]
    private float MaxMoveDistance = 1.0f;

    [SerializeField]
    private Vector3 PushZoneOffset = Vector3.zero;

    [SerializeField]
    private Vector3 PushZoneHalfExtents = Vector3.one;

    private float mInitialYPos = 0.0f;

    private void Start()
    {
        mInitialYPos = this.transform.position.y;
    }

    private void Update()
    {
        if (IsBeingStoodOn())
            Move(Dir.Down, PushSpeed);
        else
            Move(Dir.Up, ExtendSpeed);

        if (IsBottomedOut()) print("Bottomed Out!");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(this.transform.position + PushZoneOffset, PushZoneHalfExtents * 2.0f);

        Gizmos.DrawCube(this.transform.position - new Vector3(0.0f, MaxMoveDistance, 0.0f), new Vector3(1.0f, 0.05f, 1.0f));
    }

    private void Move(Dir direction, float speed)
    {
        speed *= direction == Dir.Up ? 1.0f : -1.0f;

        var pos = this.transform.position;
        pos.y = Mathf.Clamp(pos.y + speed * Time.deltaTime, mInitialYPos - MaxMoveDistance, mInitialYPos);
        this.transform.position = pos;
    }

    private bool IsBeingStoodOn()
    {
        var rayhits = Physics.BoxCastAll(this.transform.position + PushZoneOffset, PushZoneHalfExtents, this.transform.TransformDirection(Vector3.up), Quaternion.identity, PushZoneHalfExtents.y);
        foreach (var hit in rayhits)
            if (hit.collider.tag == "Player")
                return true;
        return false;
    }

    private bool IsBottomedOut()
    {
        return this.transform.position.y <= mInitialYPos - MaxMoveDistance;
    }
}
