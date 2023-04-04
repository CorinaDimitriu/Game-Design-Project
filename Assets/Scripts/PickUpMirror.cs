using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpMirror : MonoBehaviour
{
	public float TheDistance;
	public GameObject ExtraCross;
	public GameObject ActionDisplay;
	public GameObject ActionText;
	public GameObject Mirror;
	public GameObject MirrorTrigger;
	public GameObject MirrorCopy;
	public GameObject inventoryManager;

	void Update()
	{
		TheDistance = PlayerCasting.DistanceFromTarget;
	}
	void OnMouseOver()
	{
		if (TheDistance <= 6)
		{
			ExtraCross.SetActive(true);
			ActionText.GetComponent<Text>().text = "Pick up the Mirror";
			ActionDisplay.GetComponent<Text>().text = "[K]";
			ActionDisplay.SetActive(true);
			ActionText.SetActive(true);
		}
		if (Input.GetButtonDown("PickUp"))
		{
			if (TheDistance <= 6)
			{
				this.GetComponent<BoxCollider>().enabled = false;
				ActionDisplay.SetActive(false);
				ActionText.SetActive(false);
				ExtraCross.SetActive(false);
				Mirror.SetActive(false);
				//MirrorCopy.SetActive(true);
				inventoryManager.GetComponent<GeneralKeyboardActions>().enabledObjects[1] = true;
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
