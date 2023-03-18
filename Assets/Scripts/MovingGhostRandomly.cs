using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
   public class MovingGhostRandomly : MonoBehaviour
   {
     public NavMeshAgent agent;
     public ThirdPersonCharacter character;
     public GameObject Cylinderunu;
     public enum State
     {
      PATROL,
      CHASE
     }
     public State state;
     private bool alive;
     //variables for patrolling
     public GameObject[] waypoints;
     public int waypointInd;
     public float patrolSpeed=4f;
     //variables for chasing
     public float chaseSpeed=5f;
     public GameObject target;
     void Start()
     {
      agent=GetComponent<NavMeshAgent>();
      character=GetComponent<ThirdPersonCharacter>();
      agent.updatePosition = true;
      agent.updateRotation = false;
      waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
      waypointInd = Random.Range(0, waypoints.Length);
      state = MovingGhostRandomly.State.PATROL;
      alive=true;
      StartCoroutine("FSM");
     }

     IEnumerator FSM()
     {
       while(alive)
       {
        switch(state)
        {
          case State.PATROL:
               Patrol();
               break;
          case State.CHASE:
               Chase();
               break;
        }
        yield return null;
       }
     }

     void Patrol()
     {
       agent.speed = patrolSpeed;
       if (Vector3.Distance (this.transform.position, waypoints[waypointInd].transform.position) >=2 )
           {
            agent.SetDestination(waypoints[waypointInd].transform.position);
            character.Move(agent.desiredVelocity,false,false);
           }
           else if(Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) <2 )
           {
            waypointInd = Random.Range(0, waypoints.Length);
           }
           else
           {
            character.Move(Vector3.zero, false, false);
           }
     }

     void Chase()
     {
       agent.speed=chaseSpeed;
       agent.SetDestination(target.transform.position);
       character.Move(agent.desiredVelocity,false,false);
     }

     void OnTriggerEnter(Collider col)
     {
       if(col.tag == "Play")
          {
           state=MovingGhostRandomly.State.CHASE;
           target=col.gameObject;
          }
     }
     void OnTriggerExit(Collider col)
     {
       if(col.tag == "Play")
          {
           Cylinderunu.SetActive(false);
          }
     }
   }
}
