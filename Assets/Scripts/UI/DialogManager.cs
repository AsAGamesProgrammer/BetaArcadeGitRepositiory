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

    //Dialogues
    public DialogueArray[] dialogueLeo;
    public DialogueArray[] dialogueCat;
    public DialogueArray[] dialoguePanda;
    public DialogueArray[] dialogueMinimap;

    //Minimap
    public bool playMinimapDialogue = false;
    public DisableMinimap minimapScript;
    private bool minimapFound = false;

    //UI hints
    public ShowHint hints;

    void Start ()
    {
        Tia = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Check for a dialogue
        checkButtons();

        //Check if player talked to everyone
        if (!minimapFound)
            playMapDialogue();

    }

    //Check if a dialogue has to be played and play if required
    void checkButtons()
    {
        //CAT
        //If button is pressed and associated dialogue is not completed
        if (catButton.IsBottomedOutAndLocked() && !catDialogueCompleted)
        {
            //Play a dialogue
            if (!playDialogue(dialogueCat))
            {
                dialogueIsPlaying(false);
                catDialogueCompleted = true;
            }
        }

        //PANDA
        //If button is pressed and associated dialogue is not completed
        if (pandaButton.IsBottomedOutAndLocked() && !pandaDialogueCompleted)
        {
            //Play a dialogue
            if (!playDialogue(dialoguePanda))
            {
                dialogueIsPlaying(false);
                pandaDialogueCompleted = true;
            }
        }

        //LEO
        //If button is pressed and associated dialogue is not completed
        if (leoButton.IsBottomedOutAndLocked() && !leoDialogueCompleted)
        {
            //Play a dialogue
            if (!playDialogue(dialogueLeo))
            {
                dialogueIsPlaying(false);
                leoDialogueCompleted = true;
            }
        }
    }

    void dialogueIsPlaying(bool isPlaying)
    {
        //Show or Hide a hint
        hints.setJumpVisible(isPlaying);

        //Show/Hide dialogue UI
        dialogueCanvas.SetActive(isPlaying);

        //Don't allow/Allow a pause
        pauseScript.canBePaused = !isPlaying;

        //Ignore/Enable Tia's input
        Tia.SetIgnoreInput(isPlaying);

        if(!isPlaying)
            currentLine = 0;
    }

    //Play a certain dialogue passed as a parameter
    bool playDialogue(DialogueArray[] playedDialogue)
    {
        //Prepare for a dialogue
        dialogueIsPlaying(true);

        //Click enabled
        if (Input.GetButtonDown("Jump"))
        {
            nextClick();
        }

        //Check if the dialogue is at the end
        if (currentLine < playedDialogue.Length)
        {
            characterName.text = playedDialogue[currentLine].dialogueLine[0];
            characterLine.text = playedDialogue[currentLine].dialogueLine[1];
            return true;
        }
        else
            return false;
    }

    //Play a minimapDialogue
    void playMapDialogue()
    {
        if (leoDialogueCompleted && catDialogueCompleted && pandaDialogueCompleted)
        {
            if (playMinimapDialogue)
            {
                minimapScript.enabled = true;
                
                //Play a dialogue
                if (currentLine + 1 >= dialogueMinimap.Length)
                {
                    //Hints
                    hints.setJumpVisible(false);
                    hints.setMinimapVisible(true);

                    //Make a player press B
                    if (Input.GetButtonDown("Minimap"))
                    {
                        dialogueIsPlaying(false);
                        playMinimapDialogue = false;
                        minimapFound = true;

                        hints.setMinimapVisible(false);
                    }
                }
                else
                    playDialogue(dialogueMinimap);
            }
        }
        else
        {
            playMinimapDialogue = false;
        }
    }

    //Btn NEXT click
    public void nextClick()
    {
        //Debug.Log("Click");
        currentLine++;
    }
}

[System.Serializable]
public class DialogueArray
{
    [Tooltip("Add name of the character, then a line")]
    public string[] dialogueLine;
}
