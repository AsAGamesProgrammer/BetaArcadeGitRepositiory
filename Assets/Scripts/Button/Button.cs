using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    enum Dir {Up, Down}

    [SerializeField]
    [Tooltip("The tag of the player object (used when detecting if the player is standing on the button)")]
    private string PlayerTag = "Player";

    [SerializeField]
    [Range(0.0f, 5.0f)]
    [Tooltip("The speed at which the button will descend")]
    private float PushSpeed = 1.0f;

    [SerializeField]
    [Range(0.0f, 5.0f)]
    [Tooltip("The speed at which the button will ascend")]
    private float ExtendSpeed = 1.0f;

    [SerializeField]
    [Range(0.0f, 5.0f)]
    [Tooltip("The maximum distance the button can move before bottoming out")]
    private float MaxMoveDistance = 1.0f;

    [SerializeField]
    [Tooltip("The centre of the player detection zone")]
    private Vector3 PushZoneCentre = Vector3.zero;

    [SerializeField]
    [Tooltip("The size of the player detection zone")]
    private Vector3 PushZoneSize = Vector3.one;

    [SerializeField]
    [Tooltip("Should the button stay bottomed out once the player stops standing on it")]
    private bool LockPositionOnBottomOut = true;

    private float mInitialYPos = 0.0f;
    private bool mIsPosLocked = false;


    //-------------------------------------------Unity Functions-------------------------------------------

    private void Start()
    {
        // Recording the buttons initial y position.
        mInitialYPos = this.transform.position.y;
    }

    private void FixedUpdate()
    {
        // Moving the button down if it is being stood on.
        if (IsBeingStoodOn())
            Move(Dir.Down, PushSpeed);

        // Extending the button up if it is not being stood on.
        else
            Move(Dir.Up, ExtendSpeed);

        // Locking the button in posiiton if it is bottomed out.
        if (IsBottomedOut() && LockPositionOnBottomOut)
            LockButtonPosition(true);
    }

    private void OnDrawGizmos()
    {
        // Drawing a magenta cube to represent the player detection area used in the box cast.
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(this.transform.position + PushZoneCentre, PushZoneSize * 2.0f);

        // Drawing a flat magenta cube to represent the min position of the button.
        var initPos = this.transform.position;
        if (Application.isPlaying) initPos.y = mInitialYPos;
        var maxMoveCentre = initPos - new Vector3(0.0f, MaxMoveDistance, 0.0f);
        Gizmos.DrawCube(maxMoveCentre, new Vector3(1.0f, 0.05f, 1.0f));
    }


    //-------------------------------------------Public Functions------------------------------------------

    public void LockButtonPosition(bool lockPos)
    {
        // Setting whether the button pos is locked or not.
        mIsPosLocked = lockPos;
    }

    public bool IsBottomedOutAndLocked()
    {
        // Determing if the button is at or below its minimum vertical pos.
        return IsBottomedOut() && mIsPosLocked;
    }

    public bool IsBottomedOut()
    {
        // Determing if the button is at or below its minimum vertical pos.
        return this.transform.position.y <= mInitialYPos - MaxMoveDistance;
    }


    //------------------------------------------Private Functions------------------------------------------

    private void Move(Dir direction, float speed)
    {
        // Returning early if the button is pos locked.
        if (mIsPosLocked) return;

        // Determining the direction of vertical movement.
        speed *= direction == Dir.Up ? 1.0f : -1.0f;

        // Moving the button and clamping it between its min and max vertical positions.
        var pos = this.transform.position;
        pos.y = Mathf.Clamp(pos.y + speed * Time.deltaTime, mInitialYPos - MaxMoveDistance, mInitialYPos);
        this.transform.position = pos;
    }

    private bool IsBeingStoodOn()
    {
        // Firing a box cast out the top of the button to determine if the player is stood on top of it.
        var rayhits = Physics.BoxCastAll(this.transform.position + PushZoneCentre, PushZoneSize, this.transform.TransformDirection(Vector3.up), Quaternion.identity, (PushZoneSize * 2.0f).y);
        foreach (var hit in rayhits)
            if (hit.collider.tag == "Player")
                return true;
        return false;
    }
}