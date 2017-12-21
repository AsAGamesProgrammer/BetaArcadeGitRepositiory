using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPuzzleManager : MonoBehaviour {

    [HideInInspector]
    public bool PuzzleComplete = false;

    [SerializeField]
    [Tooltip("The buttons used in this puzzle in the order they need ot be pressed")]
    private List<Button> ButtonsInPuzzle = new List<Button>();

    [SerializeField]
    [Tooltip("The button that will unlock all buttons in the puzzle")]
    private Button ResetButton;

    [SerializeField]
    [Tooltip("Should the puzzle buttons reset if the player presses them in the wrong order?")]
    private bool ResetOnLoose = false;

    private List<int> PressOrderIndexes = new List<int>();


    //-------------------------------------------Unity Functions-------------------------------------------

    private void Update()
    {
        // Reseting the level if the reset button is pressed.
        if (ResetButton != null && ResetButton.IsBottomedOut())
            ResetPuzzle();

        // Exiting early if the puzzle is already complete.
        if (PuzzleComplete) return;

        // Checking if any buttons have been pressed since the last update.
        CheckForButtonPresses();

        // Checking if all buttons have been pressed.
        if (AllButtonsBottomedOutAndLocked())
        {
            // Marking the puzzle as complete if the buttons were pressed in the right order.
            if (OrderIsCorrect())
                PuzzleComplete = true;
            // Reseting the puzzle if the buttons were pressed in the wrong order.
            else if(ResetOnLoose)
                ResetPuzzle();

            print(PuzzleComplete);
        }

    }


    //-------------------------------------------Public Functions------------------------------------------

    public void ResetPuzzle()
    {
        // Reseting the order that the buttons were pressed in.
        PressOrderIndexes = new List<int>();

        // Unlocking all buttons in the puzzle.
        foreach (var button in ButtonsInPuzzle)
            button.LockButtonPosition(false);

        // Reseting the 'Puzzle Complete' variable.
        PuzzleComplete = false;
    }


    //------------------------------------------Private Functions------------------------------------------

    private void CheckForButtonPresses()
    {
        // Looping through all buttons in the puzzle.
        for(int i = 0; i < ButtonsInPuzzle.Count; i++)
        {
            // Recording the button as pressed if it has not already been done.
            var button = ButtonsInPuzzle[i];
            if (button != null && button.IsBottomedOut() && !PressOrderIndexes.Contains(i))
            {
                PressOrderIndexes.Add(i);
                if (!OrderIsCorrect())
                    ResetPuzzle();
            }
        }
    }

    private bool AllButtonsBottomedOutAndLocked()
    {
        // Looping through the buttons in the puzzle checking for any that are not bottomed out and locked.
        foreach (var button in ButtonsInPuzzle)
            if (button != null && !button.IsBottomedOutAndLocked())
                return false;
        return true;
    }

    private bool OrderIsCorrect()
    {
        for(int i = 0; i < PressOrderIndexes.Count; i++)
            if (i != PressOrderIndexes[i])
                return false;
        return true;
    }
}
