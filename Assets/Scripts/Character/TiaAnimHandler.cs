using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiaAnimHandler : MonoBehaviour {

	public void DeathAnimComplete()
    {
        FindObjectOfType<Player>().IsDoneDying = true;
    }

    public void StepOccured()
    {
        FindObjectOfType<Player>().Step();
    }
}
