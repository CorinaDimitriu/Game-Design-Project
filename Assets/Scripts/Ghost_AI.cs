using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_AI : MonoBehaviour
{
    public GameObject ThePlayer;
    public GameObject TheEnemy;
    public bool AttackTrigger = false;
    public bool IsAttacking = false;
    public AudioSource HurtSound;
    public GameObject TheFlash;

	void Update ()
	{
        if(AttackTrigger == true && IsAttacking == false)
           {
            StartCoroutine(InflictDamage());
           }
	}

	void OnTriggerEnter(Collider col)
	{
	    if(col.tag == "Sph")
	       AttackTrigger=true;
	}

	void OnTriggerExit(Collider col)
	{
	    if(col.tag == "Sph")
	       AttackTrigger=false;
	}

	IEnumerator InflictDamage()
	{
	 IsAttacking = true;
     TheFlash.SetActive(true);
     HurtSound.Play();
     yield return new WaitForSeconds(0.2f);
     TheFlash.SetActive(false);
	 yield return new WaitForSeconds(1.1f);
	 GlobalHealth.currentHealth -= 5;
	 yield return new WaitForSeconds(0.9f);
	 IsAttacking = false;
	}
}
