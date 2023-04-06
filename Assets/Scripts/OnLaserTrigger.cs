using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLaserTrigger : MonoBehaviour
{
    public GameObject laser;
    public GameObject Ghost;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Mirror_Sensitive")
        {
            laser.SetActive(false);
            laser.GetComponent<CapsuleCollider>().isTrigger = false;
        }
        else if (other.tag == "Sph")
        {
            this.GetComponent<AudioSource>().Play();
            Ghost.GetComponent<MovingGhostRandomly>().state = MovingGhostRandomly.State.SEARCH_MUSEUM;
        }
    }
}
