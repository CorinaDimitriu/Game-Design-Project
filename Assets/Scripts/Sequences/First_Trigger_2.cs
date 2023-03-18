using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class First_Trigger_2 : MonoBehaviour
{
     public GameObject ThePlayer;
     public GameObject TextBox;
     public GameObject TheMarker;
     public GameObject Trigger;


     void OnTriggerEnter ()
     {
      this.GetComponent<BoxCollider>().enabled = false;
      ThePlayer.GetComponent<FirstPersonController>().enabled = false;
      StartCoroutine(ScenePlayer());
     }

     IEnumerator ScenePlayer ()
    {
        TextBox.GetComponent<Text>().text="Looks like a weapon on that table!";
        yield return new WaitForSeconds(2.5f);
        TextBox.GetComponent<Text>().text="";
        ThePlayer.GetComponent<FirstPersonController>().enabled = true;
        TheMarker.SetActive(true);
    }

}
