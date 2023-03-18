using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalHealth : MonoBehaviour
{

     public static int currentHealth;
     public int internalHealth;

     void Start()
     {
       currentHealth = 5;
     }
	 void Update ()
	{
	    internalHealth = currentHealth;
		if(internalHealth <= 0)
		   SceneManager.LoadScene(2);


	}
}
