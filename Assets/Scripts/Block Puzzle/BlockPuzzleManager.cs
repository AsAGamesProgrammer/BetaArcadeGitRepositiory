﻿using System.Collections;
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


public class BlockPuzzleManager : MonoBehaviour {    


    [SerializeField]
    private Vector2 GridDims = Vector2.one;
    [SerializeField]
    private int TileCountX = 1;
    [SerializeField]
    private int TileCountY = 1;
    [SerializeField]
    private List<StreamLauncher> StreamLaunchers = new List<StreamLauncher>();

    private Vector2 pTileDims { get { return new Vector2(GridDims.x / (float)TileCountX, GridDims.y / (float)TileCountY); } }
    private List<Block> mBlocks = new List<Block>();
    private bool mPuzzleActive = false;


    //-------------------------------------------Unity Functions-------------------------------------------

    private void Start()
    {
        foreach (var block in FindObjectsOfType<Block>())
            mBlocks.Add(block);
        SetStreamLaunchersActive(false);
    }

    private void Update()
    {
        if (!mPuzzleActive) return;

        bool hasWon = true;
        foreach (var block in mBlocks)
            if (!block.IsOnTarget())
                hasWon = false;

        if (hasWon) OnPuzzleComplete();
    }

    private void OnDrawGizmos()
    {
        var tiles = GetTilePositions();
        foreach (var tile in tiles)
        {             
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(tile.Pos, new Vector3(pTileDims.x, 1f, pTileDims.y));
            //if (tile.TileState == BlockPuzzleTileState.Invalid) Gizmos.color = Color.red;
            //if (tile.TileState == BlockPuzzleTileState.Player) Gizmos.color = Color.blue;
            //Gizmos.DrawCube(tile.Pos, new Vector3(pTileDims.x * 0.3f, 8f, pTileDims.y * 0.3f));
        }
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

        foreach (var block in mBlocks)
            block.OnPuzzleComplete();

        SetStreamLaunchersActive(false);
    }

    public Vector3 GetNearestTilePos(Vector3 pos)
    {
        var tiles = GetTilePositions();
        Vector3 tilePos = Vector3.one * float.PositiveInfinity;

        foreach (var tile in tiles)
            if (Vector3.Distance(pos, tile.Pos) < Vector3.Distance(pos, tilePos))
                tilePos = tile.Pos;

        return tilePos;
    }

    public bool BlockMoveIsValid(Vector3 blockPos, BlockDirection dir, out BlockPuzzleTileInfo tileInfo)
    {
        tileInfo = new BlockPuzzleTileInfo();
        var tiles = GetTilePositions();

        BlockPuzzleTileInfo newTile = new BlockPuzzleTileInfo();
        newTile.Pos = Vector3.one * float.PositiveInfinity;
        newTile.TileState = BlockPuzzleTileState.Invalid;

        float alignThreshold = 0.1f;

        foreach(var tile in tiles)
        {
            bool tileAlignsCorrectly = false;

            switch(dir)
            {
                case BlockDirection.Forward:
                    if (tile.Pos.z > blockPos.z && Mathf.Abs(tile.Pos.x - blockPos.x) < alignThreshold)
                        tileAlignsCorrectly = true;
                    break;
                case BlockDirection.Right:
                    if (tile.Pos.x > blockPos.x && Mathf.Abs(tile.Pos.z - blockPos.z) < alignThreshold)
                        tileAlignsCorrectly = true;
                    break;
                case BlockDirection.Backward:
                    if (tile.Pos.z < blockPos.z && Mathf.Abs(tile.Pos.x - blockPos.x) < alignThreshold)
                        tileAlignsCorrectly = true;
                    break;
                case BlockDirection.Left:
                    if (tile.Pos.x < blockPos.x && Mathf.Abs(tile.Pos.z - blockPos.z) < alignThreshold)
                        tileAlignsCorrectly = true;
                    break;
                case BlockDirection.Null:
                    return false;
            }

            if (tileAlignsCorrectly && Vector3.Distance(blockPos, tile.Pos) < Vector3.Distance(blockPos, newTile.Pos))
                newTile = tile;        
        }

        tileInfo = newTile;

        // Checking that if Tia is pushed back she wont be forced into an invalid tile.
        if (tileInfo.TileState == BlockPuzzleTileState.Player)
        {
            BlockPuzzleTileInfo tempTile;
            bool validMove = BlockMoveIsValid(newTile.Pos, dir, out tempTile);
            bool onGrid = GridContainsPoint(tileInfo.Pos + new Vector3(DirectionToVector(dir).x * pTileDims.x, 0f, DirectionToVector(dir).z * pTileDims.y));
            return (validMove || !onGrid);
        }
         
        if (tileInfo.TileState == BlockPuzzleTileState.Valid)
            return true;
        return false;
    }


    //-------------------------------------------Public Functions------------------------------------------

    public static Vector3 DirectionToVector(BlockDirection dir)
    {
        switch (dir)
        {
            case BlockDirection.Forward:
                return Vector3.forward;
            case BlockDirection.Backward:
                return Vector3.back;
            case BlockDirection.Right:
                return Vector3.right;
            case BlockDirection.Left:
                return Vector3.left;
            default:
                return Vector3.zero;
        }
    }


    //------------------------------------------Private Functions------------------------------------------

    private BlockPuzzleTileInfo[] GetTilePositions()
    {
        BlockPuzzleTileInfo[] tiles = new BlockPuzzleTileInfo[TileCountX * TileCountY];

        Vector3 startPoint = transform.position - (new Vector3(GridDims.x, 0f, GridDims.y) / 2f) + (new Vector3(pTileDims.x, 0f, pTileDims.y) / 2f);

        for(int i = 0; i < TileCountX; i++)
        {
            for(int j = 0; j < TileCountY; j++)
            {
                var tilePos = startPoint + new Vector3(pTileDims.x * i, 0f, pTileDims.y * j);
                tiles[i * TileCountY + j].Pos = tilePos;
                tiles[i * TileCountY + j].TileState = BlockPuzzleTileState.Invalid;

                RaycastHit hit;
                if (Physics.BoxCast(tilePos + Vector3.up * 10f, new Vector3(pTileDims.x, 1f, pTileDims.y) * 0.4f, Vector3.down, out hit, Quaternion.identity))
                {
                    if (hit.collider.tag == "Block Puzzle Tile")
                        tiles[i * TileCountY + j].TileState = BlockPuzzleTileState.Valid;
                    else if (hit.collider.tag == "Player")                    
                        tiles[i * TileCountY + j].TileState = BlockPuzzleTileState.Player;
                }
            }
        }

        return tiles;
    }

    private bool GridContainsPoint(Vector3 point)
    {
        float left = transform.position.x - GridDims.x / 2f;
        float right = transform.position.x + GridDims.x / 2f;
        float top = transform.position.z + GridDims.y / 2f;
        float bottom = transform.position.z - GridDims.y / 2f;

        return (point.x <= right && point.x >= left && point.z <= top && point.z >= bottom);
    }

    private void SetStreamLaunchersActive(bool active)
    {
        foreach (var launcher in StreamLaunchers)
            launcher.gameObject.SetActive(active);
    }
}