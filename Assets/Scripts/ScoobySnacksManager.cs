using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoobySnacksManager : MonoBehaviour
{
    private GameObject[] waypoints;
    private int waypointInd;
    public GameObject inventoryManager;
    private float TheDistance;
    public GameObject ExtraCross;
    public GameObject ActionDisplay;
    public GameObject ActionText;
    public GameObject CookieCopy;
    public GameObject Cookie;
    private bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("Snack");
        waypointInd = Random.Range(0, waypoints.Length);
    }

    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
        if (inventoryManager.GetComponent<GeneralKeyboardActions>().enabledObjects[2] == false
            && flag == false)
        {
            waypointInd = Random.Range(0, waypoints.Length);
            StartCoroutine(ManageSnacks());
        }
    }

    void OnMouseOver()
    {
        if (TheDistance <= 6)
        {
            ExtraCross.SetActive(true);
            ActionText.GetComponent<Text>().text = "Wow! Scooby Snack!";
            ActionDisplay.GetComponent<Text>().text = "[K]";
            ActionDisplay.SetActive(true);
            ActionText.SetActive(true);
        }
        if (Input.GetButtonDown("PickUp"))
        {
            if (TheDistance <= 6)
            {
                this.GetComponent<BoxCollider>().enabled = false;
                ActionDisplay.SetActive(false);
                ActionText.SetActive(false);
                ExtraCross.SetActive(false);
                Cookie.SetActive(false);
                //CookieCopy.SetActive(true);
                inventoryManager.GetComponent<GeneralKeyboardActions>().enabledObjects[2] = true;
            }
        }
    }

    void OnMouseExit()
    {
        ExtraCross.SetActive(false);
        ActionDisplay.SetActive(false);
        ActionText.SetActive(false);
    }

    IEnumerator ManageSnacks()
    {
        flag = true;
        Cookie.SetActive(true);
        Cookie.transform.position = waypoints[waypointInd].transform.position;
        yield return new WaitForSeconds(30f);
        flag = false;
    }
}
