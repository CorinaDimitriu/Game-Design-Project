using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PickUpKeys : MonoBehaviour
{
    public float TheDistance;
    public GameObject ActionDisplay;
    public GameObject ActionText;
    public GameObject ThePlayer;
    public GameObject Key;
    public GameObject KeyTrigger;
    public GameObject ExtraCross;
    public GameObject M9;
    public GameObject PistolTrigger;
    public GameObject FirstTrigger;
    public GameObject TextBox;
    public GameObject FadeScreenIn;
    public Text CountText;
    public Text WinText;
    private static int count;

    void Start ()
    {
     count=0;
     SetCountText();
    }

	void Update ()
	{
		TheDistance = PlayerCasting.DistanceFromTarget;
	}

	void OnMouseOver ()
	{
	    if (TheDistance <= 1.8)
	        {
	         ExtraCross.SetActive(true);
	         ActionText.GetComponent<Text>().text="Pick up the Key";
	         ActionDisplay.GetComponent<Text>().text="[K]";
	         ActionDisplay.SetActive(true);
	         ActionText.SetActive(true);
	        }
        if (Input.GetButtonDown("PickUp"))
            {
             if(TheDistance <= 1.8)
                {this.GetComponent<BoxCollider>().enabled = false;
                 ActionDisplay.SetActive(false);
	             ActionText.SetActive(false);
	             ExtraCross.SetActive(false);
	             Key.SetActive(false);
	             count++;
	             SetCountText();
                }
            }
	}

	void SetCountText()
	{
	  CountText.text = "Count: " + count.ToString();
      if (count >= 12)
        {
            SceneManager.LoadScene(3);
        }
	}

	void OnMouseExit()
	{
	  ExtraCross.SetActive(false);
	  ActionDisplay.SetActive(false);
      ActionText.SetActive(false);
	}

    IEnumerator ScenePlayer ()
    {
        yield return new WaitForSeconds (2);
        FadeScreenIn.SetActive(false);
        WinText.text="";
        Key.SetActive(false);
    }


}
