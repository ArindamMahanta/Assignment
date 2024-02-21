using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rowText;
    [SerializeField] TextMeshProUGUI columnText;


    public void UpdateRowAndColumnText(int row, int column){

        rowText.text = "Row: "+ row.ToString();
        columnText.text = "Column: "+ column.ToString();
    }
}
