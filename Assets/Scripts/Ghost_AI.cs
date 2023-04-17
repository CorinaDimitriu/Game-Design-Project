using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Ghost_AI : MonoBehaviour
{
    public GameObject ThePlayer;
    public GameObject TheEnemy;
    public float proximityThreshold = 10.0f;
    public bool AttackTrigger = false;
    public bool IsAttacking = false;
    public AudioSource HurtSound;
    public GameObject TheFlash;
    public GameObject changeCharacter;
    public AudioSource ScoobySound;
    public AudioSource VelmaSound;
    public AudioSource DaphneSound;
    public AudioSource ShaggySound;
    private GameObject canvas;
    private Text text;
    private bool flag = false;
    public GameObject Cookie;
    public GameObject inventoryManager;
    public GameObject[] waypoints;
    public Vector3 positionP;
    public Vector3 positionE;
    public bool flagPos = false;


    private void Start()
    {
        canvas = GameObject.FindGameObjectsWithTag("Canvas")[0];
        text = canvas.GetComponentsInChildren<Text>().Where(txt => txt.name.Contains("Box")).ToArray()[0];
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
    }
    void Update ()
	{
        if(IsAttacking)
        {
            TheEnemy.transform.parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            TheEnemy.transform.parent.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            TheEnemy.transform.parent.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
            TheEnemy.transform.parent.transform.position = positionE;
        }
        if(AttackTrigger == true && IsAttacking == false)
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
        yield return new WaitForSeconds(3.0f);
        text.text = "";
        flag = false;
    }

    void OnTriggerEnter(Collider col)
	{
        if (col.tag == "Sph")
        {
            if (!flagPos)
            {
                flagPos = true;
                positionP = ThePlayer.transform.position;
                positionE = TheEnemy.transform.parent.transform.position;
                //TheEnemy.transform.parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                ThePlayer.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                //TheEnemy.transform.parent.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                //TheEnemy.transform.parent.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                //TheEnemy.transform.parent.transform.position = positionE;
                ThePlayer.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                ThePlayer.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                ThePlayer.transform.position = positionP;
            }
            AttackTrigger = true;
        }
	}

	void OnTriggerExit(Collider col)
	{
        if (col.tag == "Sph")
        {
            AttackTrigger = false;
            if(IsAttacking == false)
            {
                flagPos = false;
                //TheEnemy.transform.parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                ThePlayer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }
	}


    private void LoseLife()
    {
        Image[] lifeImages = canvas.GetComponentsInChildren<Image>().Where(img => img.name.Contains("Lives")).ToArray();

        for (int i = 0; i < lifeImages.Length; i++)
        {
            if ((i + 1) * 5 <= GlobalHealth.currentHealth)
            {
                lifeImages[i].enabled = true;
            }
            else
            {
                lifeImages[i].enabled = false;
            }
        }
    }

    IEnumerator InflictDamage()
	{
		 IsAttacking = true;
		 TheFlash.SetActive(true);
		 HurtSound.Play();
		 yield return new WaitForSeconds(0.2f);
		 TheFlash.SetActive(false);
		 yield return new WaitForSeconds(0.9f);
         if (Cookie.activeSelf == false)
         {
            GlobalHealth.currentHealth -= 5;
            LoseLife();
        }
         else
         {
            inventoryManager.GetComponent<GeneralKeyboardActions>().enabledObjects[2] = false;
            Cookie.SetActive(false);
         }
         //change ghost position to a random waypoint
         int randomIndex = Random.Range(0, waypoints.Length);
         //get the parent GameObject of TheEnemy
         GameObject parent = TheEnemy.transform.parent.gameObject;
         //get a random waypoint
         parent.transform.position = waypoints[randomIndex].transform.position;
         GetComponent<MovingGhostRandomly>().state = MovingGhostRandomly.State.PATROL;
         IsAttacking = false;
    }
}
