using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    int tileRow = 0;
    int tileColumn = 0;
    float maxRayDistance = 100f;

    UIManager ui;

    void Start(){

        ui = FindObjectOfType<UIManager>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hitInfo; 
        
        if (Physics.Raycast(ray, out hitInfo, maxRayDistance))
        {
            if(hitInfo.collider.tag == "Tile"){

                tileRow = hitInfo.collider.GetComponent<Tile>().row;
                tileColumn = hitInfo.collider.GetComponent<Tile>().column;

                //Debug.Log("Row: "+ tileRow +" Column: "+ tileColumn);

                ui.UpdateRowAndColumnText(tileRow, tileColumn);

                // Visual feedback of collision detection
                //hitInfo.collider.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
}
