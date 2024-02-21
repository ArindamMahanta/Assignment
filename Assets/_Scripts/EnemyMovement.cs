using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, AI
{
    [SerializeField] float moveSpeed = 5f;
    bool isMoving;

    GameObject player;
    PlayerMovement playerInfo;

    GridGenerator gridInfo;
    List<Tile> currentPath = new List<Tile>();
    int currentPathIndex;



    void Start(){

        player = GameObject.FindGameObjectWithTag("Player");
        playerInfo = player.GetComponent<PlayerMovement>();

        playerInfo.AddListenerToPlayerMovedEvent(MoveTowardsPlayer);

        gridInfo = FindObjectOfType<GridGenerator>();
        gridInfo.GetTile(transform.position).isWalkable = false;
    }

    void Update(){

        //MoveTowardsPlayer();
    }

    public void MoveTowardsPlayer(){

        Debug.Log("PlayerMovedEvent invoked");
        if(player != null /*&& !playerInfo.isMoving && !isMoving*/){

            Tile startTile = gridInfo.GetTile(transform.position);
            Tile playerTile = gridInfo.GetTile(this.player.transform.position);

            List<Tile> targetTiles = playerTile.Neighbours;
            Debug.Log("Possible no. of targets ="+ targetTiles.Count);

            int index = 0;

            while(index < targetTiles.Count && (targetTiles[index] == null || !targetTiles[index].isWalkable)){
                index = Random.Range(0,targetTiles.Count-1);
            }

            Debug.Log("Starting from: start row = "+ startTile.row + " start column = "+ startTile.column);
            Debug.Log("Target chosen: target row = "+ targetTiles[index].row + " target column = "+ targetTiles[index].column);

            currentPath = Pathfinding.FindPath(startTile, targetTiles[index]);
            currentPath.Reverse();
            currentPathIndex = 0;

            Debug.Log("Current path count: "+ currentPath.Count);
            
            if (currentPath != null)
            {
                startTile.isWalkable = true;
                // Start moving along the path
                StartCoroutine(MoveAlongPath());
            }
        }
    }

    private IEnumerator MoveAlongPath()
    {
        isMoving = true;

        while (currentPathIndex < currentPath.Count)
        {
            //Debug.Log("Current tile no. ="+ currentPathIndex);
            Vector3 targetPosition = currentPath[currentPathIndex].transform.position;

            while (transform.position != new Vector3(targetPosition.x, 1.5f, targetPosition.z))
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, transform.position.y, targetPosition.z), moveSpeed * Time.deltaTime);
                yield return null;
            }

            currentPathIndex++;
        }

        gridInfo.GetTile(transform.position).isWalkable = false;
        isMoving = false;
    }
}
