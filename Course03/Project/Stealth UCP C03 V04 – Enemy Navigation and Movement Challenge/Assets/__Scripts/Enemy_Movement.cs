using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;//For NavMesh

//---------------------------------------------------------------------
//
//  Enemy_Movement.cs
//
//  What does it do ?
//
//  Runs a cycle, separated in 5 steps. Rotation_Current, Waiting, Rotation_Next, Walking.
//
// 1. Rotation_Current : Rotates the player to current waypoint's orientation
// 2. Waiting : Waits the given time on the waypoint
// 3. Rotation_Next : Rotates the enemy towards next waypoint
// 4. Walking : Sets the enemy's destination. When reached, loops to Rotation_Current.
//
//
//---------------------------------------------------------------------

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy_Movement : MonoBehaviour
{
    //The nav mesh agent of the enemy
    private NavMeshAgent _agent;

    //Enum used to separate the enemy state
    public enum EnemyState { Waiting, Walking, Rotation_Next, Rotation_Current };

    //Variable that stores the enemy's state
    public EnemyState state;

    //Rotation speed used for rotating towards current or next waypoint
    public float rotationSpeed;
    private float timer;
    private float timeToWait;

    //Waypoints
    public Transform WaypointsParent;
    private List<Transform> l_waypoints = new List<Transform>();
    private int currentWaypointID;
    
    //Order of waypoints to follow by the enemy
    public List<int> l_WaypointsOrder = new List<int>();
    private bool sens = false;

    // Start is called before the first frame update
    void Start()
    {
        _agent = this.GetComponent<NavMeshAgent>();

        //Starts the game by rotating to the current waypoint
        state = EnemyState.Rotation_Current;

        //Adds every waypoints to a list
        setAllWaypoints();

        //First waypoint is 1
        currentWaypointID = l_waypoints[1].GetComponent<Waypoints>().Waypoint_ID;

        timer = 0f;
        timeToWait = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        // 1. Rotates the player to current's waypoint's orientation.
        if(state == EnemyState.Rotation_Current)
        {
            if(!isDoneRotatingToCurrentWaypoint())
            {
                RotateToCurrentWaypoint();
            }
            else
            {
                //When it's done, state is changed to "Waiting"
                state = EnemyState.Waiting;
                timeToWait = l_waypoints[currentWaypointID].GetComponent<Waypoints>().Waypoint_SecondsToWaitAt;
            }
           
        }

        // 2. Waits the waypoints given time
        if(state == EnemyState.Waiting)
        {
            timer += Time.deltaTime;
            if(timer >= timeToWait)
            {
                //When the wait is over, sets state to next step
                timer = 0f;
                state = EnemyState.Rotation_Next;
                AssignNextWaypoint();
            }
        }

        // 3. Rotates towards next waypoint
        if(state == EnemyState.Rotation_Next)
        {

            if (!isDoneRotatingToNextWaypoint())
            {
                RotateToNextPoint();
            }
            else
            {
                // 4. Then the rotation is over, sets the destination and goes there.
                _agent.destination = l_waypoints[currentWaypointID].position;
                if(!_agent.pathPending)
                {
                    state = EnemyState.Walking;
                }
                
            }
        }
        
        // 5. When the enemy gets close to the target waypoint, set its state to the first step -> End of cycle.
        if(state == EnemyState.Walking)
        {
            if(_agent.remainingDistance < 0.1f)
            {
                state = EnemyState.Rotation_Current;
            }
        }

        


    }

    /// <summary>
    /// Bool that returns whether the enemy has the current waypoint orientation or not
    /// </summary>
    private bool isDoneRotatingToCurrentWaypoint()
    {
        if (transform.rotation == l_waypoints[currentWaypointID].rotation)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Rotate the enemy towards the current waypoint's orientation
    /// </summary>
    private void RotateToCurrentWaypoint()
    {
        Quaternion target = l_waypoints[currentWaypointID].rotation;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Adds all waypoints' transforms to a list
    /// </summary>
    void setAllWaypoints()
    {
        foreach (Transform waypoints in WaypointsParent)
        {
            l_waypoints.Add(waypoints);
            //Debug.Log(WaypointsList[WaypointsList.Count-1] + " added to Waypoints List");
        }
    }

    /// <summary>
    /// Rotates the enemy towards the next waypoint
    /// </summary>
    void RotateToNextPoint()
    {
        Quaternion target = Quaternion.LookRotation(l_waypoints[currentWaypointID].position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotationSpeed * Time.deltaTime);
    }


    /// <summary>
    /// Returns whether the enemy is facing the next waypoint or not
    /// </summary>
    /// <returns></returns>
    private bool isDoneRotatingToNextWaypoint()
    {

        Vector3 dirFromAtoB = (l_waypoints[currentWaypointID].position - transform.position).normalized;

        float dot = Vector3.Dot(dirFromAtoB, transform.forward);

        if (dot > 0.999f)
        {
            return true;
        }
        else
        {
            return false;
        }

    }


    /// <summary>
    /// Determines the next waypoint according to the list.
    /// </summary>
    private void AssignNextWaypoint()
    {
        if (currentWaypointID == l_WaypointsOrder.Count - 1)
        {
            sens = false;
        }
        if (currentWaypointID == 0)
        {
            sens = true;
        }
        if (sens)
        {
            currentWaypointID++;
        }
        else
        {
            currentWaypointID--;
        }
    }
}
