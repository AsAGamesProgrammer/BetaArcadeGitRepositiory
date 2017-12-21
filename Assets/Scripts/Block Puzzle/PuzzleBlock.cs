using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBlock : Block {

    [SerializeField]
    private Transform targetTrans;


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
}
