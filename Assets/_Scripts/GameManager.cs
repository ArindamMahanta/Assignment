using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject enemyPrefab;

    void Start()
    {
        SpawnPlayer();
        SpawnEnemy();
    }

    void SpawnPlayer(){

        Instantiate(playerPrefab, new Vector3(0, 1.5f, 0), Quaternion.identity);    
    }

    void SpawnEnemy(){

        Instantiate(enemyPrefab, new Vector3(9, 1.5f, 9), Quaternion.identity);    
    }
}
