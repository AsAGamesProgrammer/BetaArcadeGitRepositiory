using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    // Use this for initialization

    //Player
    private Player Tia;

    //Buttons
    public Button catButton;
    public Button pandaButton;
    public Button leoButton;

    //Canvas
    public GameObject dialogueCanvas;
    public Text characterName;
    public Text characterLine;

    //marks all the dialogues completed
    private bool catDialogueCompleted = false;
    private bool pandaDialogueCompleted = false;
    private bool leoDialogueCompleted = false;

    //Pause menu
    public pauseGame pauseScript;

    private int currentLine = 0;

    public DialogueArray[] dialogueCat;
        //if button pressed
        //Initiate a dialogue
        //For the number of elements, show name/phrase 

	void Start ()
    {
        Tia = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (catButton.IsBottomedOutAndLocked())
        {
            Debug.Log("Button pressed");
            if (!catDialogueCompleted)
            {
                //Prepare for a dialogue
                dialogueIsPlaying(true);
                //Play a dialogue
                playDialogue(dialogueCat);
                //Finish dialogue
                //dialogueIsPlaying(false);
            }
        }
	}

    void dialogueIsPlaying(bool isPlaying)
    {
        //Show/Hide dialogue UI
        dialogueCanvas.SetActive(isPlaying);

        //Don't allow/Allow a pause
        pauseScript.canBePaused = !isPlaying;

        //Ignore/Enable Tia's input
        Tia.SetIgnoreInput(isPlaying);
    }

    //void startDialogue(DialogueArray[] playedDialogue)
    //{
    //    //Show dialogue UI
    //    dialogueCanvas.SetActive(true);

    //    //Don't allow a pause
    //    pauseScript.canBePaused = false;

    //    //Ignore Tia's input
    //    Tia.SetIgnoreInput(true);

    //    playDialogue(playedDialogue);
    //}

    void playDialogue(DialogueArray[] playedDialogue)
    {
        //Click enabled
        if (Input.GetButtonDown("Jump"))
        {
            nextClick();
        }

        if (currentLine < playedDialogue.Length)
        {
            characterName.text = playedDialogue[currentLine].dialogueLine[0];
            characterLine.text = playedDialogue[currentLine].dialogueLine[1];
        }
        else
            dialogueIsPlaying(false);

    }

    //void finishDialogue()
    //{
    //    //Hide dialogue UI
    //    dialogueCanvas.SetActive(false);

    //    //Don't allow a pause
    //    pauseScript.canBePaused = true;

    //    //Enable Tia's input
    //    Tia.SetIgnoreInput(false);
    //}

    //Btn NEXT click
    public void nextClick()
    {
        Debug.Log("Click");
        currentLine++;
    }
}

[System.Serializable]
public class DialogueArray
{
    [Tooltip("Add name of the character, then a line")]
    public string[] dialogueLine;
}
