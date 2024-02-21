using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] ObstacleGrid obstacleGrid;
    [SerializeField] GameObject obstaclePrefab;

    /// <summary>
    /// Gets the boolean value of the specified position on the obstacle grid
    /// </summary>
    /// <param name="row">obstacle grid row</param>
    /// <param name="column">obstacle frid column</param>
    /// <returns></returns>
    public bool GetObstacleData(int row, int column){

        bool obstacleStatus = obstacleGrid.GetObstacleStatus(row, column);
        return obstacleStatus;
    }

    /// <summary>
    /// Spawns obstacle prefab at the specified position on the obstacle grid
    /// </summary>
    /// <param name="row">obstacle grid row</param>
    /// <param name="column">obstacle grid column</param>
    public void SpawnObstacle(int row, int column){

        Instantiate(obstaclePrefab, new Vector3(row, 1, column), Quaternion.identity);
    }
}
