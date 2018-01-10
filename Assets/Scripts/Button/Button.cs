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

    [SerializeField]
    [Range(0.0f, 10.0f)]
    [Tooltip("The radius within which the button will asist the player to the centre")]
    private float GravityRadius = 2.0f;

    [SerializeField]
    [Tooltip("The magnitude of the force applied to the player when they are in the gravity radius")]
    private float GravityMultiplier = 2.0f;

    [SerializeField]
    private AudioClip SFX;

    private float mInitialYPos = 0.0f;
    private bool mIsPosLocked = false;
    private bool mIgnorePlayerWhenExtending = false;
    private bool mApplyGravity = true;
    private bool mCanSound = true;


    //-------------------------------------------Unity Functions-------------------------------------------

    private void Start()
    {
        // Recording the buttons initial y position.
        mInitialYPos = this.transform.position.y;
    }

    private void Update()
    {
        // Moving the button down if it is being stood on.
        if (IsBeingStoodOn())
        {
            Move(Dir.Down, PushSpeed);
            if (mCanSound && !IsBottomedOut())
            {
                GetComponent<AudioSource>().PlayOneShot(SFX);
                mCanSound = false;
            }
        }

        // Extending the button up if it is not being stood on.
        else
        {
            Move(Dir.Up, ExtendSpeed);
            if(!mIgnorePlayerWhenExtending)
                GetComponent<AudioSource>().Stop();
            mCanSound = true;
        }

        // Locking the button in posiiton if it is bottomed out.
        if (IsBottomedOut() && LockPositionOnBottomOut)
            LockButtonPosition(true);

        // Applying a force to the player if they are within gravity range.
        if (PlayerIsInGravityRange() && mApplyGravity)
        {
            // Checking for edge cases before applying the force.
            if (!IsBeingStoodOn() || ((Mathf.Abs(Input.GetAxis("Horizontal")) < 0.01f && Mathf.Abs(Input.GetAxis("Vertical")) < 0.01f)))
            {
                // Calculating and applying the gravity force.                
                var player =FindObjectOfType<Player>();
                if(player != null)
                {
                    var movementVector = this.transform.position - player.transform.position;
                    var forceVector = movementVector.normalized * 1.0f / Mathf.Max(0.01f, movementVector.magnitude);
                    forceVector.y = 0.0f;
                    player.PullToButtonCentre(forceVector * GravityMultiplier);
                }
            }
        }

        // No longer applying the player pull force if the button has been bottomed out.
        if (IsBottomedOut())
            mApplyGravity = false;
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

        // Drawing a magenta wire sphere to show the gravity area.
        Gizmos.DrawWireSphere(this.transform.position + PushZoneCentre, GravityRadius);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Reseting the 'mIgnorePlayerWhenExtending' and 'mPullPlayerToCentre' variables if the player 
        // has come into contact with the button again.
        if (collision.gameObject.tag == PlayerTag)
        {
            mIgnorePlayerWhenExtending = false;
            mApplyGravity = true;
        }
    }


    //-------------------------------------------Public Functions------------------------------------------

    public void LockButtonPosition(bool lockPos)
    {
        // Setting whether the button pos is locked or not.
        mIsPosLocked = lockPos;

        // Ignoring the player so the button can reset without the player stopping it.
        if (!lockPos)
        {
            print("Hello World");
            mIgnorePlayerWhenExtending = true;
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().PlayOneShot(SFX);
        }
    }

    public bool IsBottomedOutAndLocked()
    {
        // Determing if the button is at or below its minimum vertical pos.
        return IsBottomedOut() && mIsPosLocked;
    }

    public bool IsBottomedOut()
    {
        // Determing if the button is at or below its minimum vertical pos.
        return Mathf.Abs(this.transform.position.y - (mInitialYPos - MaxMoveDistance)) <= 0.01f;
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
        pos.y = Mathf.Clamp(pos.y + speed * Time.deltaTime, mInitialYPos - MaxMoveDistance - float.Epsilon, mInitialYPos);
        this.transform.position = pos;
    }

    private bool IsBeingStoodOn()
    {
        // Exiting early if the player should be ignored.
        if (mIgnorePlayerWhenExtending) return false;

        // Firing a box cast out the top of the button to determine if the player is stood on top of it.
        var rayhits = Physics.BoxCastAll(this.transform.position + PushZoneCentre, PushZoneSize, this.transform.TransformDirection(Vector3.up), Quaternion.identity, (PushZoneSize * 2.0f).y);
        foreach (var hit in rayhits)
            if (hit.collider.tag == "Player")
                return true;
        return false;
    }

    private bool PlayerIsInGravityRange()
    {
        // Returning true if the player is within the gravity radius.
        var playerTrans = FindObjectOfType<Player>().transform;
        if (playerTrans == null) return false;
        return (Vector3.Distance(playerTrans.position, this.transform.position + PushZoneCentre) < GravityRadius);
    }
}