using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLaserTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        this.GetComponent<AudioSource>().Play();
    }
}
