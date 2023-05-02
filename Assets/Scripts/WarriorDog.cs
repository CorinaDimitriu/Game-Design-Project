using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorDog : MonoBehaviour
{
    public GameObject player;
    public float interactionDistance = 4.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate dog to face player
        Vector3 directionToPlayer = player.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        float dogDistanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (dogDistanceToPlayer < interactionDistance)
        {
            // Change animation to "Attack01"
            GetComponent<Animator>().SetTrigger("Attack");
        }
        else
        {
            // Change animation to "Idle"
            GetComponent<Animator>().SetTrigger("Defend");
        }
    }
}
