using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class FakeGhostAI : MonoBehaviour
{
    public int lives = 3;
    public float proximityThreshold = 10.0f;
    public GameObject ThePlayer;
    public GameObject TheEnemy;
    public bool AttackTrigger = false;
    public bool IsAttacking = false;
    public AudioSource HurtSound;
    public AudioSource ScoobySound;
    public AudioSource VelmaSound;
    public AudioSource DaphneSound;
    public AudioSource ShaggySound;
    public GameObject TheFlash;
    public GameObject[] PlayerWaypoints;
    public GameObject panel;
    public GameObject changeCharacter;
    private GameObject canvas;
    private Text text;
    private bool flag = false;
    public GameObject Cookie;
    public GameObject inventoryManager;

    private void Start()
    {
        canvas = GameObject.FindGameObjectsWithTag("Canvas")[0];
        text = canvas.GetComponentsInChildren<Text>().Where(txt => txt.name.Contains("Box")).ToArray()[0];
    }

    private void Update()
    {
        if (AttackTrigger == true && IsAttacking == false)
        {
            StartCoroutine(InflictDamage());
        }

        float distance = Vector3.Distance(ThePlayer.transform.position, TheEnemy.transform.position);
        if (distance < proximityThreshold && flag == false)
        {
            flag = true;
            StartCoroutine(GetScaredAccordingly());
        }
    }

    IEnumerator GetScaredAccordingly()
    {
        switch (changeCharacter.GetComponent<ChangeCharacter>().CurrentPesrpsctive)
        {
            case "scooby": ScoobySound.Play(); text.text = "OOH-OO"; break;
            case "velma": VelmaSound.Play(); text.text = "The Snow Ghost!"; break;
            case "daphne": DaphneSound.Play(); text.text = "There's something spooky going around here!"; break;
            case "shaggy": ShaggySound.Play(); text.text = "Zoinks!"; break;
            default: break;
        }
        text.text = "";
        yield return new WaitForSeconds(3.0f);
        flag = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Sph")
            AttackTrigger = true;
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Sph")
            AttackTrigger = false;
    }


    private void ApplyDamage()
    {
        int waypointInd = Random.Range(0, PlayerWaypoints.Length);
        GameObject chosenWaypoint = PlayerWaypoints[waypointInd];
        ThePlayer.transform.position = chosenWaypoint.transform.position;
    }

    IEnumerator InflictDamage()
    {
        IsAttacking = true;
        TheFlash.SetActive(true);
        HurtSound.Play();
        yield return new WaitForSeconds(0.2f);
        TheFlash.SetActive(false);
        yield return new WaitForSeconds(1.1f);
        //panel.GetComponent<Animation>().Play();
        if(Cookie.activeSelf == false)
            ApplyDamage();
        else
        {
            inventoryManager.GetComponent<GeneralKeyboardActions>().enabledObjects[2] = false;
            Cookie.SetActive(false);
        }
        yield return new WaitForSeconds(0.9f);
        IsAttacking = false;
        GetComponent<MovingGhostRandomlyFake>().state = MovingGhostRandomlyFake.State.PATROL;
    }
}
