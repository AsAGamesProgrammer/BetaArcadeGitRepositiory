using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPuzzleManager : MonoBehaviour {

    [SerializeField]
    private Vector2 GridDims = Vector2.one;
    [SerializeField]
    private int TileCountX = 1;
    [SerializeField]
    private int TileCountY = 1;

    private Vector2 pTileDims { get { return new Vector2(GridDims.x / (float)TileCountX, GridDims.y / (float)TileCountY); } }


    //-------------------------------------------Unity Functions-------------------------------------------

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        var tiles = GetTilePositions();
        foreach (var tile in tiles)
            Gizmos.DrawWireCube(tile, new Vector3(pTileDims.x, 0f, pTileDims.y));
    }


    //------------------------------------------Private Functions------------------------------------------

    private Vector3[] GetTilePositions()
    {
        Vector3[] tiles = new Vector3[TileCountX * TileCountY];

        Vector3 startPoint = transform.position - (new Vector3(GridDims.x, 0f, GridDims.y) / 2f) + (new Vector3(pTileDims.x, 0f, pTileDims.y) / 2f);

        for(int i = 0; i < TileCountX; i++)
        {
            for(int j = 0; j < TileCountY; j++)
            {
                tiles[i + j] = startPoint + new Vector3(pTileDims.x * i, 0f, pTileDims.y * j);
            }
        }

        return tiles;
    }
}
