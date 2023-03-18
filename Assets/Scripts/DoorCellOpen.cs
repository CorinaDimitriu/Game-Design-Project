using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorCellOpen : MonoBehaviour
{
    public float TheDistance;
    public GameObject ActionDisplay;
    public GameObject ActionText;
    public GameObject TheHinge;
    public AudioSource CreekSound;
    public GameObject ExtraCross;

	void Update ()
	{
		TheDistance = PlayerCasting.DistanceFromTarget;
	}

	void OnMouseOver ()
	{
	    if (TheDistance <= 1)
	        {
	         ExtraCross.SetActive(true);
	         ActionDisplay.SetActive(true);
	         ActionText.SetActive(true);
	        }
        if (Input.GetButtonDown("Action"))
            {
             if(TheDistance <= 1)
                {this.GetComponent<BoxCollider>().enabled = false;
                 ActionDisplay.SetActive(false);
	             ActionText.SetActive(false);
	             TheHinge.GetComponent<Animation>().Play("OpenDoorAnim1");
	             CreekSound.Play();
                }
            }
	}

	void OnMouseExit()
	{
	  ExtraCross.SetActive(false);
	  ActionDisplay.SetActive(false);
      ActionText.SetActive(false);
	}

}
