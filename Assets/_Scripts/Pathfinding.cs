using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Class for executing A* pathfinding 
/// </summary>
public static class Pathfinding 
{
    public static List<Tile> FindPath(Tile startTile, Tile targetTile){

        var toSearch = new List<Tile>() { startTile };
        var processed = new List<Tile>();

        //Debug.Log(startTile);

        while(toSearch.Any()){

            var current = toSearch[0];

            //Debug.Log("Search tiles: "+ toSearch.Count());
            //selecting next best tile
            foreach(var tile in toSearch){

                if(tile.f < current.f || tile.f==current.f && tile.h<current.h){

                    current = tile;
                }
            }

            processed.Add(current);
            toSearch.Remove(current);

            //backtracking to start after reaching target and then returning the path
            if(current == targetTile){

                var currentPathTile = targetTile;
                var path = new List<Tile>();

                while(currentPathTile != startTile){

                    path.Add(currentPathTile);
                    currentPathTile = currentPathTile.Connection;
                }

                return path;
            }

            //adding current tile's neighbours to search list  
            foreach(var neighbour in current.Neighbours.Where(t => t.isWalkable && !processed.Contains(t))){

                var inSearch = toSearch.Contains(neighbour);

                var costToNeighbour = current.g + current.GetDistance(neighbour);

                if(!inSearch || costToNeighbour < neighbour.g){

                    neighbour.SetG(costToNeighbour);
                    neighbour.SetConnection(current);

                    if(!inSearch){

                        neighbour.SetH(neighbour.GetDistance(targetTile));
                        toSearch.Add(neighbour);
                    }
                }
            }
        }

        return null;
    }
}
