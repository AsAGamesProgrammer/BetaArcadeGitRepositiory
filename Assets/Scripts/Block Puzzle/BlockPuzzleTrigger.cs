using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPuzzleTrigger : MonoBehaviour {

    [SerializeField]
    private BlockPuzzleManager PuzzleManager;


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            PuzzleManager.StartPuzzle();
            Destroy(gameObject);
        }
    }
}
