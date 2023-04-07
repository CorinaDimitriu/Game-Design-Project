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

    private void Start()
    {
        canvas = GameObject.FindGameObjectsWithTag("Canvas")[0];
        text = canvas.GetComponentsInChildren<Text>().Where(txt => txt.name.Contains("Box")).ToArray()[0];
    }
    void Update ()
	{
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
	    if(col.tag == "Sph")
	       AttackTrigger=true;
	}

	void OnTriggerExit(Collider col)
	{
	    if(col.tag == "Sph")
	       AttackTrigger=false;
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
		 yield return new WaitForSeconds(1.1f);
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
		 yield return new WaitForSeconds(1.5f);
		 IsAttacking = false;
         GetComponent<MovingGhostRandomly>().state = MovingGhostRandomly.State.PATROL;
    }
}
