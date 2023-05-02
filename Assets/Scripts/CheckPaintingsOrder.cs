using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPaintingsOrder : MonoBehaviour
{
    public GameObject key;
    public GameObject[] frames;
    private GameObject[] paintings;
    private bool missionOver = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!missionOver)
        {
            StartCoroutine(CheckOrder());
        }
    }

    IEnumerator CheckOrder()
    {
        paintings = new GameObject[frames.Length + 1];
        bool allPaintingsSet = false;

        while (true)
        {
            yield return new WaitForSeconds(2f);
            // Get the paintings from the frames
            for (int i = 0; i < frames.Length; i++)
            {
                if (frames[i].transform.childCount > 1 && i != 4)
                {
                    paintings[i] = frames[i].transform.GetChild(1).gameObject;
                    allPaintingsSet = true;
                }
                else if (frames[i].transform.childCount > 2 && i == 4)
                {
                    paintings[i] = frames[i].transform.GetChild(2).gameObject;
                    allPaintingsSet = true;
                }
                else
                {
                    allPaintingsSet = false; 
                    break;
                }
            }

            if (!allPaintingsSet)
            {
                continue;
            }

            //Debug.Log(paintings[0].tag);
            //Debug.Log(paintings[1].tag);
            //Debug.Log(paintings[2].tag);
            //Debug.Log(paintings[3].tag);
            //Debug.Log(paintings[4].tag);
            paintings[paintings.Length - 1] = transform.GetChild(1).gameObject;
            if (paintings[0].tag == "fred" && paintings[1].tag == "velma" 
                && paintings[2].tag == "shaggy" && paintings[3].tag == "daphne" && paintings[4].tag == "scooby")
            {
                key.SetActive(true);
                missionOver = true;
                break;
            }
        }
    }
}
