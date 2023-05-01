using UnityEngine;

public class Slide : MonoBehaviour
{
    public GameObject fpc;
    public AudioSource SlideSource;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Sph")
        {
            SlideSource.Play();
            fpc.GetComponent<FirstPersonController>().isSliding = true;
            fpc.GetComponent<FirstPersonController>().slideTimer = 5;
        }
    }
}
