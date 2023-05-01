using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TriggerGhostStory : MonoBehaviour
{
    public GameObject inventoryManager;
    public GameObject TheEnemy;
    public bool IsLocked = false;
    public GameObject canvas;
    public GameObject TheFlash;

    private void Start()
    {
        canvas = GameObject.FindGameObjectsWithTag("Canvas")[0];
    }
    IEnumerator LoseLife()
    {
        GlobalHealth.currentHealth -= 5;
        Image[] lifeImages = canvas.GetComponentsInChildren<UnityEngine.UI.Image>().Where(img => img.name.Contains("Lives")).ToArray();
        TheFlash.SetActive(true);
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
        yield return new WaitForSeconds(0.2f);
        TheFlash.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (inventoryManager.GetComponent<GeneralKeyboardActions>().enabledObjects[5])
        {
            if (other.tag == "Sph")
            {
                //lose life
                StartCoroutine(LoseLife());
            }
            else if(other.tag == "Enemy")
            {
                //lock ghost
                IsLocked = true;
                StartCoroutine(LockGhost());
            }
        }

    }

    IEnumerator LockGhost()
    {
        Debug.Log("Lock");
        //TheEnemy.transform.parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        //TheEnemy.transform.parent.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        //TheEnemy.transform.parent.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        //TheEnemy.transform.parent.position = new Vector3(-51.6300011f, TheEnemy.transform.parent.position.y, 7.76999998f);
        TheEnemy.transform.parent.GetComponent<NavMeshAgent>().SetDestination(new Vector3(-51.6300011f,
            TheEnemy.transform.parent.position.y, 7.76999998f));
        TheEnemy.transform.parent.GetComponent<ThirdPersonCharacter>().
            Move(TheEnemy.transform.parent.GetComponent<NavMeshAgent>().desiredVelocity, false, false);
        yield return new WaitForSeconds(2f);
    }
}
