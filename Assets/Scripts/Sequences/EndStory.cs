using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndStory : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayEndStory());
    }

    IEnumerator PlayEndStory()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<AudioSource>().Play();
    } 
}
