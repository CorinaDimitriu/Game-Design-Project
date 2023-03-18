using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger_3 : MonoBehaviour
{
      public AudioSource DoorBang;
      public AudioSource DoorJumpMusic;
      public GameObject TheZombie;
      public GameObject TheDoor1;

      void OnTriggerEnter()
      {
       GetComponent<BoxCollider>().enabled=false;
       TheDoor1.GetComponent<Animation>().Play("JumpDoorAnim");
       DoorBang.Play();
       TheZombie.SetActive(true);
       StartCoroutine(PlayJumpMusic());
      }

      IEnumerator PlayJumpMusic()
      {
       yield return new WaitForSeconds(0.4f);
       DoorJumpMusic.Play();
      }

}
