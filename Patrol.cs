using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    float currentTime = 0f;
    float startTime = 5f;
    public enum StateType { state_patrol, state_evade};
    public StateType currState = StateType.state_patrol;

    private Rigidbody rb;
    private NavMeshAgent nav;
    public PlayerController pacmanTarget;

    public GameObject[] escapePoints;
    private GameObject evadePoint;


    public GameObject[] patrolPoints;//however many you want - must be on navmesh
    private int cuurentPoint;

    void Start()
    {
        currentTime = startTime;
        cuurentPoint = 0;
    }

    public void Update()
    {
        rb = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        UpdateState(currState);
    }

    public void ChangeState(StateType newState)
    {
        currState = newState;
    }

    public void UpdateState(StateType CurrentState)
    {
        switch (CurrentState)
        {
            case StateType.state_evade:
                currentTime -= 1 * Time.deltaTime;

                if (currentTime <= 0)
                {
                    currentTime = 0;
                    pacmanTarget.dangerous = false;
                    ChangeState(StateType.state_patrol);
                }
                EvadeEnemy();
                break;

            case StateType.state_patrol:
                if (pacmanTarget.dangerous)
                {
                    ChangeState(StateType.state_evade);
                    currentTime = 5;
                }
                Patrolling();
                //stateDisplay.text = "Evade";
                break;

        }//end switch

    }

    void Patrolling()
    {
        //keep track of which one you have been to last
        //start ant index 0 and then increment target
        //set index 0 as destination - then test to see distancebetween pacman and target - if that dist is smaller than x then increment counter
        //use that counter to set next destination e.g. setDest(patrolPoints[counter].position)
        //don't let counter get > number of elements in the array if it gets bigger - reset to 0
        if (Vector3.Distance(this.transform.position, patrolPoints[cuurentPoint].transform.position) <= 1f)
        {
            Iterate();
        }
        nav.SetDestination(patrolPoints[cuurentPoint].transform.position);
    }

    void Iterate()
    {
        if(cuurentPoint<patrolPoints.Length-1)
        {
            cuurentPoint++;
        }
        else
        {
            cuurentPoint = 0;
        }
        nav.SetDestination(patrolPoints[cuurentPoint].transform.position);
    }


    public void EvadeEnemy()
    {
        //Debug.Log("I am evading");
        //actually evade
        //check which escape point is furthest away from pacman and set that as our target
        float furthestDis = 0;

        foreach (GameObject ep in escapePoints)
        {
            if (Vector3.Distance(ep.transform.position, pacmanTarget.gameObject.transform.position) > furthestDis)
            {
                furthestDis = Vector3.Distance(ep.transform.position, pacmanTarget.gameObject.transform.position);
                evadePoint = ep;
            }
        }
        nav.SetDestination(evadePoint.transform.position);
    }
}