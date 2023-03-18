using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Opening_1 : MonoBehaviour
{
    public GameObject ThePlayer;
    public GameObject FadeScreenIn;
    public GameObject TextBox;

	void Start ()
	{
		ThePlayer.GetComponent<FirstPersonController>().enabled = false;
		StartCoroutine(ScenePlayer());
    }
    IEnumerator ScenePlayer ()
    {
        yield return new WaitForSeconds (1.5f);
        FadeScreenIn.SetActive(false);
        TextBox.GetComponent<Text>().text="I need to get out of here!";
        yield return new WaitForSeconds (2);
        TextBox.GetComponent<Text>().text="";
        ThePlayer.GetComponent<FirstPersonController>().enabled = true;
    }

}
