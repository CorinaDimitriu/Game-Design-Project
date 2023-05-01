using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//namespace UnityStandardAssets.Characters.ThirdPerson

public class MovingGhostRandomly : MonoBehaviour
{
    public NavMeshAgent agent;
    public ThirdPersonCharacter character;
    public GameObject Cylinderunu;
    public GameObject fishingNet;
    public enum State
    {
        PATROL,
        CHASE,
        SEARCH_MUSEUM
    }
    public State state;
    private bool alive;
    //variables for patrolling
    public GameObject[] waypoints;
    public GameObject[] waypointsMuseum;
    public int waypointInd;
    public int waypointIndMuseum;
    public float patrolSpeed = 4f;
    private int visited = 0;
    //variables for chasing
    public float chaseSpeed = 5f;
    public GameObject target;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<ThirdPersonCharacter>();
        agent.updatePosition = true;
        agent.updateRotation = false;
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        waypointsMuseum = GameObject.FindGameObjectsWithTag("Museum");
        waypointInd = UnityEngine.Random.Range(0, waypoints.Length);
        waypointIndMuseum = UnityEngine.Random.Range(0, waypointsMuseum.Length);
        state = MovingGhostRandomly.State.PATROL;
        alive = true;
        StartCoroutine("FSM");
    }

    IEnumerator FSM()
    {
        while (alive)
        {
            switch (state)
            {
                case State.PATROL:
                    if(!fishingNet.GetComponent<TriggerGhostStory>().IsLocked)
                    {
                        Patrol();
                    }
                    else
                    {
                        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                        //GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                        //GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                        //transform.position = new Vector3(-51.6300011f, transform.position.y, 7.76999998f);
                        agent.SetDestination(new Vector3(-51.6300011f, transform.position.y, 7.76999998f));
                        character.Move(agent.desiredVelocity, false, false);
                    }
                    break;
                case State.CHASE:
                    if (!fishingNet.GetComponent<TriggerGhostStory>().IsLocked)
                        Chase();
                    else
                    {
                        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                        //GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                        //GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                        //transform.position = new Vector3(-51.6300011f, transform.position.y, 7.76999998f);
                        agent.SetDestination(new Vector3(-51.6300011f, transform.position.y, 7.76999998f));
                        character.Move(agent.desiredVelocity, false, false);
                    }
                    break;
                case State.SEARCH_MUSEUM:
                    if (!fishingNet.GetComponent<TriggerGhostStory>().IsLocked)
                        SearchMuseum();
                    else
                    {
                        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                        //GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                        //GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                        //transform.position = new Vector3(-51.6300011f, transform.position.y, 7.76999998f);
                        agent.SetDestination(new Vector3(-51.6300011f, transform.position.y, 7.76999998f));
                        character.Move(agent.desiredVelocity, false, false);
                    }
                    break;
            }
            yield return null;
        }
    }

    void Patrol()
    {
        //if (!this.GetComponent<Ghost_AI>().IsAttacking)
        {
            agent.speed = patrolSpeed;
            if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) >= 2)
            {
                agent.SetDestination(waypoints[waypointInd].transform.position);
                character.Move(agent.desiredVelocity, false, false);
            }
            else if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) < 2)
            {
                waypointInd = UnityEngine.Random.Range(0, waypoints.Length);
            }
            else
            {
                character.Move(Vector3.zero, false, false);
            }
        }
    }

    void SearchMuseum()
    {
        agent.speed = patrolSpeed;
        if (Vector3.Distance(this.transform.position, waypointsMuseum[waypointIndMuseum].transform.position) >= 2)
        {
            agent.SetDestination(waypointsMuseum[waypointIndMuseum].transform.position);
            character.Move(agent.desiredVelocity, false, false);
        }
        else if (Vector3.Distance(this.transform.position, waypointsMuseum[waypointIndMuseum].transform.position) < 2)
        {
            waypointIndMuseum = UnityEngine.Random.Range(0, waypointsMuseum.Length);
            if (visited + 1 >= waypointsMuseum.Length / 2.0)
            {
                visited = 0;
                state = MovingGhostRandomly.State.PATROL;
            }
            else visited++;
        }
        else
        {
            character.Move(Vector3.zero, false, false);
        }
    }

    void Chase()
    {
        //if (!this.GetComponent<Ghost_AI>().IsAttacking)
        {
            agent.speed = chaseSpeed;
            agent.SetDestination(target.transform.position);
            character.Move(agent.desiredVelocity, false, false);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Play")
        {
            state = MovingGhostRandomly.State.CHASE;
            target = col.gameObject;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Play")
        {
            state = MovingGhostRandomly.State.PATROL;
            //Cylinderunu.SetActive(false);
        }
    }
}
