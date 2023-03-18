using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Butterflies : MonoBehaviour
{
      public AudioSource Wind;
      public AudioSource BangSound;
      public GameObject Butterfly;
      public GameObject TheDoor1;
      public GameObject JumpTrigger001;
      public float Speed = 0.1f;
      public GameObject ThePlayer;

      void OnTriggerEnter(Collider other)
      {
       if(other.gameObject.tag == "Play")
          {GetComponent<BoxCollider>().enabled=false;
           TheDoor1.GetComponent<Animation>().Play("JumpDoorAnim");
           BangSound.Play();
           Wind.Play();
           Butterfly.transform.LookAt(ThePlayer.transform);
           Butterfly.transform.position = Vector3.MoveTowards(transform.position,ThePlayer.transform.position,Speed);
          }

      }

      void OnTriggerExit(Collider other)
      {
       if(other.gameObject.tag == "Play")
          JumpTrigger001.SetActive(false);
      }
}
