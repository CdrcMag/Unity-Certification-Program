using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;//For NavMesh



[RequireComponent(typeof(NavMeshAgent))]
public class Enemy_Movement : MonoBehaviour
{
    //Enemy agent
    private NavMeshAgent _agent;

    //Destination
    private Transform _goal;

    //Waypoints
    public Transform Waypoints;
    private List<Transform> WaypointsList = new List<Transform>();

    //List of waypoint to follow in that order
    public List<int> WaypointsToFollow = new List<int>();
    private int _nextWaypoint = 0;
    bool sens = true;


    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        //Every waypoints are stored in the list
        setAllWaypoints();

        _nextWaypoint = WaypointsToFollow[0];

        goToWaypoints(_nextWaypoint);


    }

    

    private void Update()
    {
        MovementLogic();
    }




    /// <summary>
    /// Handles the enemy's movements
    /// </summary>
    void MovementLogic()
    {

        // Check if we've reached the destination
        if (!_agent.hasPath)//If the enemy doesn't have a path
        {

            int time = 0;

            time = WaypointsList[_nextWaypoint].GetComponent<Waypoints>().Waypoint_SecondsToWaitAt;

            if (_nextWaypoint == WaypointsToFollow.Count - 1)
            {
                sens = false;
            }
            if (_nextWaypoint == 0)
            {
                sens = true;
            }

            Debug.Log("Reached waypoint " + _nextWaypoint);

            if (sens)
            {
                _nextWaypoint++;
            }
            else
            {
                _nextWaypoint--;

            }

            

            goToWaypoints(_nextWaypoint);

            StartCoroutine(WaitOnPosition(time));
            
        }
    }

    IEnumerator WaitOnPosition(int time)
    {
        _agent.isStopped = true;
        yield return new WaitForSeconds(time);
        _agent.isStopped = false;

    }



    /// <summary>
    /// Moves the enemy to the waypoint identified by its id
    /// </summary>
    void goToWaypoints(int id)
    {
        _agent.destination = WaypointsList[id].transform.position;
    }



    /// <summary>
    /// Adds all waypoints' transforms to a list
    /// </summary>
    void setAllWaypoints()
    {
        foreach(Transform waypoints in Waypoints)
        {
            WaypointsList.Add(waypoints);
            //Debug.Log(WaypointsList[WaypointsList.Count-1] + " added to Waypoints List");
        }
    }

   
}
