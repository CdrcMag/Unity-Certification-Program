using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [Tooltip("To add a new waypoint, copy paste one where you want, give it the appropriate ID, and add this id to the list in the Enemy_Movement.cs component")]
    [Header("Waypoint ID (0 to X) see tooltip")]
    public int Waypoint_ID;
    [Header("Time spent not moving when position reached")]
    public int Waypoint_SecondsToWaitAt;



    private void Awake()
    {
        DeactivateVisuals();

    }

    private void DeactivateVisuals()
    {
        this.GetComponent<MeshRenderer>().enabled = false;
        this.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
    }

}
