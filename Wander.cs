using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    float currentTime = 0f;
    float startTime = 5f;
    public enum StateType { state_wander, state_evade};
    public StateType currState = StateType.state_wander;

    private Rigidbody rb;
    private NavMeshAgent nav;
    public PlayerController pacmanTarget;

    public GameObject[] escapePoints;
    private GameObject evadePoint;

    void Start()
    {
        currentTime = startTime;
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
                    ChangeState(StateType.state_wander);
                }
                EvadeEnemy();
                break;

            case StateType.state_wander:
                if (pacmanTarget.dangerous)
                {
                    ChangeState(StateType.state_evade);
                    currentTime = 5;
                }
                wandering();
                //stateDisplay.text = "Evade";
                break;

        }//end switch

    }

    private void wandering()
    {
        nav.SetDestination(getRandomPosition());
    }

    Vector3 getRandomPosition()
    {
        float x = Random.Range(-20, 20);
        float z = Random.Range(-20, 20);

        Vector3 pos = new Vector3(x, 0, z);
        return pos;
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