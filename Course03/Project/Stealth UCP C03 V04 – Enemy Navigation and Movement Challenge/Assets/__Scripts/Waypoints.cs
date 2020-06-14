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

    [HideInInspector]
    public Vector3 Waypoint_Position;
    [HideInInspector]
    public Vector3 Waypoint_Rotation;



    private void Awake()
    {
        Waypoint_Position = transform.position;
        Waypoint_Rotation = transform.rotation.eulerAngles;

        DeactivateVisuals();

    }

    private void DeactivateVisuals()
    {
        this.GetComponent<MeshRenderer>().enabled = false;
        this.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
    }

}
