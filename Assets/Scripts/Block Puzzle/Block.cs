using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    [SerializeField]
    private float PushSpeed = 1f;
    [SerializeField]
    private float InteractDistance = 3f;
    [SerializeField]
    private Transform targetTrans;

    private Vector3 mTargetPos = Vector3.zero;
    private bool mPlayerAttached = false;
    private BlockDirection mPlayerDirection = BlockDirection.Null;
    private BlockDirection mDir = BlockDirection.Null;
    private Vector3 mCurrentVelocity = Vector3.zero;
    private bool mQueueDettach = false;
    private bool mQueueDestroy = false;


    //-------------------------------------------Unity Functions-------------------------------------------

    private void Start()
    {
        var pos = FindObjectOfType<BlockPuzzleManager>().GetNearestTilePos(transform.position);
        pos.y = transform.position.y;
        transform.position = pos;
        mTargetPos = transform.position;
    }

    private void Update()
    {
        mDir = BlockDirection.Null;

        if (Input.GetButtonDown("Interact"))
        {
            var player = FindObjectOfType<Player>();
            if (player == null) return;

            if (mPlayerAttached)
            {
                mQueueDettach = true;
            }
            else
            { 
                mPlayerDirection = GetPlayerDirection();
                if ((mPlayerDirection == BlockDirection.Left || mPlayerDirection == BlockDirection.Right) && Mathf.Abs(transform.position.x - player.transform.position.x) <= InteractDistance)
                    mPlayerAttached = true;
                if ((mPlayerDirection == BlockDirection.Backward || mPlayerDirection == BlockDirection.Forward) && Mathf.Abs(transform.position.z - player.transform.position.z) <= InteractDistance)
                    mPlayerAttached = true;

                if (mPlayerAttached)
                {
                    player.SetIgnoreInput(true);
                }
            }            
        }

        if(mPlayerAttached && !mQueueDettach)
        {
            var xInput = Input.GetAxis("Horizontal");
            var yInput = Input.GetAxis("Vertical");

            if(mPlayerDirection == BlockDirection.Left || mPlayerDirection == BlockDirection.Right)
            {
                if (yInput > 0.1f) mDir = BlockDirection.Right;
                if (yInput < -0.1f) mDir = BlockDirection.Left;
            }

            else if (mPlayerDirection == BlockDirection.Backward || mPlayerDirection == BlockDirection.Forward)
            {
                if (xInput < -0.1f) mDir = BlockDirection.Forward;
                if (xInput > 0.1f) mDir = BlockDirection.Backward;
            }            

            if (mDir != BlockDirection.Null)
            {
                BlockPuzzleTileInfo tile;
                if (FindObjectOfType<BlockPuzzleManager>().BlockMoveIsValid(transform.position, mDir, out tile))
                {
                    tile.Pos.y = transform.position.y;
                    mTargetPos = tile.Pos;
                }
            }            
        }
        
        MoveToTargetPos();

        if(mPlayerAttached && FindObjectOfType<Player>())
        {
            // Update player anim.
            var player = FindObjectOfType<Player>();
            float animSpeed = mCurrentVelocity.magnitude * Mathf.Sign(Vector3.Dot(transform.position - player.transform.position, mCurrentVelocity));
            player.SetPushPullSpeed(animSpeed);
            
            // Update the player position.
            var tiaPushPos = transform.position;
            tiaPushPos.y = player.transform.position.y;
            tiaPushPos += BlockPuzzleManager.DirectionToVector(mPlayerDirection) * 2f;
            player.transform.position = Vector3.Slerp(player.transform.position, tiaPushPos, Time.deltaTime * 5f);

            // Updating the player rotation.
            player.transform.parent = transform;
            var tiaLookPos = transform.position;
            tiaLookPos.y = player.transform.position.y;
            player.transform.LookAt(tiaLookPos);

            // Dettaching the player if required.
            if (mCurrentVelocity.magnitude <= 0f)
            {
                if(mQueueDettach)
                {
                    mPlayerAttached = false;
                    player.transform.parent = null;
                    player.SetIgnoreInput(false);
                    mQueueDettach = false;
                }
            }
        }

        if(mCurrentVelocity.magnitude <= 0f && mQueueDestroy)
            Destroy(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        var pos = transform.position;
        pos += BlockPuzzleManager.DirectionToVector(mPlayerDirection) * 2f;
        Gizmos.DrawSphere(pos, .5f);
    }


    //-------------------------------------------Public Functions------------------------------------------

    public bool IsOnTarget()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
            if (hit.transform == targetTrans)
                return true;
        return false;
    }

    public void OnPuzzleComplete()
    {
        mQueueDettach = true;
        mQueueDestroy = true;
    }


    //------------------------------------------Private Functions------------------------------------------

    private void MoveToTargetPos()
    {
        var toTargetVector = mTargetPos - transform.position;
        var translation = toTargetVector.normalized * PushSpeed * Time.deltaTime;
        if (translation.magnitude > toTargetVector.magnitude)
            translation = toTargetVector;
        transform.Translate(translation);
        mCurrentVelocity = translation;
    }

    private BlockDirection GetPlayerDirection()
    {
        var player = FindObjectOfType<Player>();
        var dir = BlockDirection.Null;
        if (player == null) return dir;

        // Left / Right
        if (Mathf.Abs(player.transform.position.x - transform.position.x) < Mathf.Abs(player.transform.position.z - transform.position.z))
        {
            if (player.transform.position.z < transform.position.z)
                dir = BlockDirection.Backward;
            else
                dir = BlockDirection.Forward;
        }
        // Backward / Forward
        else
        {
            if (player.transform.position.x < transform.position.x)
                dir = BlockDirection.Left;
            else
                dir = BlockDirection.Right;
        }

        return dir;
    }
}
