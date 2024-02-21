using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private List<Tile> currentPath = new List<Tile>();
    private int currentPathIndex;
    public bool isMoving {get; private set;}

    GridGenerator gridInfo;
    PlayerMovedEvent playerMoved;

    private void Awake(){

        playerMoved = new PlayerMovedEvent();
    }

    private void Start(){

        gridInfo = FindObjectOfType<GridGenerator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && !isMoving)
        {
            // Cast a ray from the mouse position to the ground
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the ray hit a tile
                Tile clickedTile = hit.collider.GetComponent<Tile>();
                if (clickedTile != null && clickedTile.isWalkable)
                {
                    clickedTile.GetComponent<Renderer>().material.color = Color.red;

                    Debug.Log("Player POS: " + transform.position);
                    Tile startTile = gridInfo.GetTile(transform.position);
                    startTile.isWalkable = true;

                    // Find path to the clicked tile using A* pathfinding
                    currentPath = Pathfinding.FindPath(startTile, clickedTile);

                    if (currentPath != null)
                    {
                        currentPath.Reverse();
                        currentPathIndex = 0;
                        // Start moving along the path
                        StartCoroutine(MoveAlongPath());
                    }
                    else{

                        Debug.Log("No possible path found. Player is cornered!");
                    }
                }
            }
        }
    }

    private IEnumerator MoveAlongPath()
    {
        isMoving = true;

        while (currentPathIndex < currentPath.Count)
        {
            Vector3 targetPosition = currentPath[currentPathIndex].transform.position;

            while (transform.position != new Vector3(targetPosition.x, 1.5f, targetPosition.z))
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, transform.position.y, targetPosition.z), moveSpeed * Time.deltaTime);
                yield return null;
            }

            currentPathIndex++;
        }

        isMoving = false;
        
        Tile destinationTile = gridInfo.GetTile(transform.position);

        destinationTile.isWalkable = false;
        destinationTile.GetComponent<Renderer>().material.color = Color.white;

        playerMoved.Invoke();
    }

    public void AddListenerToPlayerMovedEvent(UnityAction listener){

        playerMoved.AddListener(listener);
    }
}
