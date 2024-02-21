using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Class reperesenting a tile - the fundamental unit of a grid
/// </summary>
public class Tile : MonoBehaviour
{
    GridGenerator gridInfo;
    public int row, column;
    public bool isWalkable = true;


    public List<Tile> Neighbours {get; private set;}
    public Tile Connection {get; private set;}
    public float g {get; private set;}
    public float h {get; private set;}
    public float f => g + h;



    void Start(){

        gridInfo = FindObjectOfType<GridGenerator>();
        SetNeighbours();
    }


    /// <summary>
    /// Offset values to locate neighbours of this tile
    /// </summary>
    private static readonly List<Vector2> Dirs = new List<Vector2>() {
            new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, -1), new Vector2(1, 0),
        };
    
    /// <summary>
    /// Caches all the neighbours of this tile
    /// </summary>
    public void SetNeighbours(){

        Neighbours = new List<Tile>();

        foreach(var dir in Dirs){

            var neighbourPos = new Vector3(dir.x + transform.position.x, 0, dir.y + transform.position.z);

            Tile t = gridInfo.GetTile(neighbourPos);

            if(t!=null)
            Neighbours.Add(gridInfo.GetTile(neighbourPos));
        }
    }

    /// <summary>
    /// Set given node/tile as a connection to this node/tile
    /// </summary>
    /// <param name="_tile">node/tile to set as connection</param>
    public void SetConnection(Tile _tile){

        Connection = _tile;
    }

    /// <summary>
    /// Set g cost of the tile
    /// </summary>
    /// <param name="_g">g cost value</param>
    public void SetG(float _g){

        g = _g;
    }

    /// <summary>
    /// Set h cost to the tile
    /// </summary>
    /// <param name="_h">h cost value</param>
    public void SetH(float _h){

        h = _h;
    }

    /// <summary>
    /// Gets the distance between this and the given tile
    /// </summary>
    /// <param name="_t">the tile whose distance is required</param>
    public int GetDistance(Tile _t){

        int deltaX = Mathf.Abs((int)transform.position.x - (int)_t.transform.position.x);
        int deltaY = Mathf.Abs((int)transform.position.y - (int)_t.transform.position.y);

        return deltaX + deltaY;
    }
}
