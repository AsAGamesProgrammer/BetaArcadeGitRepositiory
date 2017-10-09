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

    private List<int> PressOrderIndexes = new List<int>();


    //-------------------------------------------Unity Functions-------------------------------------------

    private void Update()
    {
        // Reseting the level if the reset button is pressed.
        if (ResetButton.IsBottomedOut())
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
            else
                ResetPuzzle();
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
            if (button.IsBottomedOut() && !PressOrderIndexes.Contains(i))
                PressOrderIndexes.Add(i);
        }
    }

    private bool AllButtonsBottomedOutAndLocked()
    {
        // Looping through the buttons in the puzzle checking for any that are not bottomed out and locked.
        foreach (var button in ButtonsInPuzzle)
            if (!button.IsBottomedOutAndLocked())
                return false;
        return true;
    }

    private bool OrderIsCorrect()
    {
        int lastIndex = -1;
        // Looping through the order that the buttons were pressed in.
        foreach(var index in PressOrderIndexes)
        {
            // If the current button has a lower index than the previous then the order was wrong.
            if (index < lastIndex)
                return false;
            // Else the order (up to this point) is correct and the loop moves on.
            lastIndex = index;
        }
        return true;
    }
}
