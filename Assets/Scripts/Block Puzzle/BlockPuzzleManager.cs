using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockDirection
{
    Forward, Backward, Right, Left, Null
}

public enum BlockPuzzleTileState
{
    Valid, Invalid, Player
}

public struct BlockPuzzleTileInfo
{
    public Vector3 Pos;
    public BlockPuzzleTileState TileState;
}


public class BlockPuzzleManager : BlockGrid {    

    [SerializeField]
    private List<StreamLauncher> StreamLaunchers = new List<StreamLauncher>();
    [SerializeField]
    protected List<PuzzleBlock> PuzzleBlocks = new List<PuzzleBlock>();

    private bool mPuzzleActive = false;


    //-------------------------------------------Kristina was here-------------------------------------------
    public bool isPuzzleActive() { return mPuzzleActive; }

    //-------------------------------------------Unity Functions-------------------------------------------

    private void Start()
    {
        //SetStreamLaunchersActive(false);
    }

    private void Update()
    {
        if (!mPuzzleActive) return;

        bool hasWon = true;
        foreach (var block in PuzzleBlocks)
            if (!block.IsOnTarget())
                hasWon = false;

        if (hasWon) OnPuzzleComplete();
    }


    //-------------------------------------------Public Functions------------------------------------------

    public void StartPuzzle()
    {
        SetStreamLaunchersActive(true);
        mPuzzleActive = true;
    }

    public void OnPuzzleComplete()
    {
        mPuzzleActive = false;

        foreach (var block in PuzzleBlocks)
            block.OnPuzzleComplete();

        SetStreamLaunchersActive(false);
    }


    //------------------------------------------Private Functions------------------------------------------

    private void SetStreamLaunchersActive(bool active)
    {
        foreach (var launcher in StreamLaunchers)
            launcher.gameObject.SetActive(active);
    }
}
