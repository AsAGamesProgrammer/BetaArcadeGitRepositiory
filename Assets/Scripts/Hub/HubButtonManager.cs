using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubButtonManager : MonoBehaviour {

    public List<Button> Buttons = new List<Button>();
	
	// Update is called once per frame
	void Update () {
        foreach (var button in Buttons)
            if (!button.IsBottomedOut())
                return;

        FindObjectOfType<OpenHubDoor>().liftDoor = true;
    }
}
