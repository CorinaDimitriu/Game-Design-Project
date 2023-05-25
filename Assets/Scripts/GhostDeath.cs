using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostDeath : MonoBehaviour
{
    public int EnemyHealth = 5;
    public GameObject TheEnemy;
    public int StatusCheck;

    void DamageGhost (int DamageAmount)
    {
     EnemyHealth -= DamageAmount;
    }

	void Update ()
	{
     if(EnemyHealth <= 0 && StatusCheck == 0)
	    StartCoroutine(KillGhost());
	}

	IEnumerator KillGhost()
	{
      this.GetComponent<Ghost_AI>().enabled = false;
      this.GetComponent<CapsuleCollider>().enabled = false;
      StatusCheck=2;
      TheEnemy.transform.position = new Vector3(transform.position.x, -3, transform.position.z);
      yield return new WaitForSeconds(1);
      SceneManager.LoadScene(5);
	}
}
