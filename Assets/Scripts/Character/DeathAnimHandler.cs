using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimHandler : MonoBehaviour {

	public void DeathAnimComplete()
    {
        FindObjectOfType<Player>().IsDoneDying = true;
    }
}
