using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGhost : MonoBehaviour
{
  public float TheDistance;
  public AudioSource Ghost_Sound;

  void Update ()
  {
   TheDistance = PlayerCasting.DistanceFromTarget;
  }

  void OnMouseOver ()
	{
	  if(TheDistance <= 200)
        {
         Ghost_Sound.Play();
        }
	}
}
