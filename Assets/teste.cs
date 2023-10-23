using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teste : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        Time.timeScale = 1f;
        if (GlobalVariables.instance != null) {
            GlobalVariables.instance.roomsVisited = 0;
            GlobalVariables.instance.lastVisitedIndex = -1;
            GlobalVariables.instance.roomsVisited = 0;
            GlobalVariables.instance.finalChoose = 0;
            GlobalVariables.instance.totalRooms = 1;        
        }
}
}
