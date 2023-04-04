using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLaserTrigger : MonoBehaviour
{
    public GameObject laser;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Mirror_Sensitive")
        {
            laser.SetActive(false);
            laser.GetComponent<CapsuleCollider>().isTrigger = false;
        }
        else
        {
            this.GetComponent<AudioSource>().Play();
        }
    }
}
