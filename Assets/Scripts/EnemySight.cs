using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
   public class EnemySight : MonoBehaviour
   {
     public NavMeshAgent agent;
     public ThirdPersonCharacter character;
     public GameObject Cylinderunu;
     public enum State
     {
      PATROL,
      CHASE,
      INVESTIGATE
     }
     public State state;
     private bool alive;
     //variables for patrolling
     public GameObject[] waypoints;
     private int waypointInd;
     public float patrolSpeed=0.5f;
     //variables for chasing
     public float chaseSpeed=1f;
     public GameObject target;
     //variables for investigating
     private Vector3 investigateSpot;
     private float timer=0;
     public float investigateWait=10;
     //variables for sight
     public float heightMultiplier;
     public float sightDistance;
     void Start()
     {
      agent=GetComponent<NavMeshAgent>();
      character=GetComponent<ThirdPersonCharacter>();
      agent.updatePosition = true;
      agent.updateRotation = false;
      waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
      waypointInd = Random.Range(0, waypoints.Length);
      state = EnemySight.State.PATROL;
      alive=true;
      heightMultiplier=1.36f;
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
          case State.INVESTIGATE:
               Investigate();
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
           else if(Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position)<=2)
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

     void Investigate()
     {
       timer+= Time.deltaTime;
       agent.SetDestination(this.transform.position);
       character.Move(Vector3.zero, false, false);
       transform.LookAt(investigateSpot);
       if(timer >= investigateWait)
          {
           state=EnemySight.State.PATROL;
           timer=0;
          }
     }

     void OnTriggerEnter(Collider col)
     {
       if(col.tag == "Play")
          {
           state=EnemySight.State.CHASE;
           target=col.gameObject;
          }
     }
     void OnTriggerExit(Collider col)
     {
       if(col.tag == "Play")
          {
           state=EnemySight.State.INVESTIGATE;
           investigateSpot=col.gameObject.transform.position;
          }
     }

     void FixedUpdate()
     {
       RaycastHit hit;
       Debug.DrawRay(transform.position+Vector3.up*heightMultiplier, transform.forward*sightDistance, Color.green);
       Debug.DrawRay(transform.position+Vector3.up*heightMultiplier, (transform.forward+transform.right).normalized*sightDistance, Color.green);
       Debug.DrawRay(transform.position+Vector3.up*heightMultiplier, (transform.forward-transform.right).normalized*sightDistance, Color.green);
       if(Physics.Raycast(transform.position+Vector3.up*heightMultiplier, transform.forward, out hit, sightDistance))
          {
           if(hit.collider.gameObject.tag == "Play")
              {
               state=EnemySight.State.CHASE;
               target=hit.collider.gameObject;
              }
          }
       if(Physics.Raycast(transform.position+Vector3.up*heightMultiplier, (transform.forward+transform.right).normalized, out hit, sightDistance))
          {
           if(hit.collider.gameObject.tag == "Play")
              {
               state=EnemySight.State.CHASE;
               target=hit.collider.gameObject;
              }
          }
       if(Physics.Raycast(transform.position+Vector3.up*heightMultiplier, (transform.forward+transform.right).normalized, out hit, sightDistance))
          {
           if(hit.collider.gameObject.tag == "Play")
              {
               state=EnemySight.State.CHASE;
               target=hit.collider.gameObject;
              }
          }
     }
   }
}
