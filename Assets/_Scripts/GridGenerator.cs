using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    public Tile[,] grid = new Tile[10,10];

    ObstacleManager obstacleManager;

    void Start()
    {
        obstacleManager = GetComponent<ObstacleManager>();
        GenerateGrid();
    }

    void GenerateGrid(){

        for(int column=0; column<10; column++){

            for(int row=0; row<10; row++){

                var tile = Instantiate(tilePrefab, new Vector3(row, 0, column), Quaternion.identity);

                tile.GetComponent<Tile>().row = row;
                tile.GetComponent<Tile>().column = column;

                // if(Random.Range(1,7) == 4){ //for quick test

                //     tile.GetComponent<Tile>().isWalkable = false;
                //     tile.GetComponent<Renderer>().material.color = Color.black;
                // }

                if(obstacleManager.GetObstacleData(row, column)){

                    //marking tile as obstacled or unwalkable to
                    tile.GetComponent<Tile>().isWalkable = false;

                    //spawning obstacle over this tile position
                    obstacleManager.SpawnObstacle(row, column);
                }

                grid[row,column] = tile.GetComponent<Tile>();
            }
        }

        Debug.Log("FROM GRIDGENERATOR: Grid Information -");
        Debug.Log("no. of tiles = "+ grid.Length);
    }

    /// <summary>
    /// Returns a tile on the grid with the given position
    /// </summary>
    /// <param name="_pos">position of the tile required</param>
    /// <returns>a tile</returns>
    public Tile GetTile(Vector3 _pos){

        foreach(var tile in grid){

            if(_pos.x == tile.row && _pos.z == tile.column){

                return tile;
            }
        }

        return null;
    }
}
