using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TARGET: trigger that enables a minimap dialogue, probably a cube at the end of the hub level
/// </summary>

public class MinimapDialogue : MonoBehaviour {

    public DialogManager dialogManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            dialogManager.playMinimapDialogue=true;
        }
    }
}
