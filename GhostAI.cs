using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{
    float currentTime = 0f;
    float startTime = 5f;
    public enum StateType { state_chase, state_evade };
	public StateType currState = StateType.state_chase;
	public Text stateDisplay;
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
                    ChangeState(StateType.state_chase);
                }
                EvadeEnemy();
                stateDisplay.text = "Evade";
				break;

			case StateType.state_chase:
				if (pacmanTarget.dangerous)
				{
                    ChangeState(StateType.state_evade);
                    currentTime = 5;
                }
     
                Chase();
				stateDisplay.text = "Chase";
				break;
		}//end switch

	}

	private void Chase()
	{
		nav.SetDestination(pacmanTarget.gameObject.transform.position);
	}

	public void EvadeEnemy()
	{
		Debug.Log("I am evading");
        //actually evade
        //check which escape point is furthest away from pacman and set that as our target
        float furthestDis = 0;

        foreach(GameObject ep in escapePoints)
        {
            if(Vector3.Distance(ep.transform.position, pacmanTarget.gameObject.transform.position) > furthestDis)
            {
                furthestDis = Vector3.Distance(ep.transform.position, pacmanTarget.gameObject.transform.position);
                evadePoint = ep;
            }
        }
        nav.SetDestination(evadePoint.transform.position);
    }


}