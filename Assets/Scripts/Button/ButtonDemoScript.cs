using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDemoScript : MonoBehaviour {

    [SerializeField]
    private GameObject WinText;

    [SerializeField]
    private ButtonPuzzleManager ButtonPuzzleManager;


    //-------------------------------------------Unity Functions-------------------------------------------

    void Update ()
    {
        WinText.SetActive(ButtonPuzzleManager.PuzzleComplete);

    }
}
