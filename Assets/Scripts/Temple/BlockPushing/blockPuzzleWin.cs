using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockPuzzleWin : MonoBehaviour {

    /// <summary>
    /// TARGET: Apply to the parent object of the blovk panels
    /// </summary>

    private recepticleCheck[] panelScripts;
    public bool puzzleSolved = false;

	// Use this for initialization
	void Start ()
    {
        int childrenCount = transform.childCount;
        panelScripts = new recepticleCheck[childrenCount];

        for (int i = 0; i < childrenCount; i++)
        {
            panelScripts[i] = transform.GetChild(i).GetComponent<recepticleCheck>();
        }

        Debug.Log(panelScripts.Length);
	}
	
	// Update is called once per frame
	void Update ()
    {
        bool flagPuzzleSolved = true;
        foreach (var panel in panelScripts)
        {
            if (!panel.getIsCorrect())
                flagPuzzleSolved = false; 
        }

        puzzleSolved = flagPuzzleSolved;

        if(puzzleSolved)
            Debug.Log("Puzzle solved "+puzzleSolved);
    }
}
