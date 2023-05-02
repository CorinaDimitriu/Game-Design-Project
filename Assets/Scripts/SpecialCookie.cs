using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialCookie : MonoBehaviour
{
    public GameObject player;
    public GameObject warriorDog;
    public GameObject hiddenPainting;
    public Text interactionKey;
    public Text interactionMessage;
    public float interactionDistance = 2f;
    private bool isInRange;
    private bool isPickedUp = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float cookieDistanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        float cookieDistanceToWarriorDog = Vector3.Distance(warriorDog.transform.position, transform.position);

        if (cookieDistanceToPlayer < interactionDistance && !isPickedUp)
        {
            if (!isInRange)
            {
                isInRange = true;
                interactionKey.gameObject.SetActive(true);
                interactionMessage.gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUpCookie();
            }
        }
        else if (cookieDistanceToWarriorDog < interactionDistance && isPickedUp)
        {
            if (!isInRange)
            {
                isInRange = true;
                interactionKey.gameObject.SetActive(true);
                interactionMessage.gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                GiveCookieToWarriorDog();
            }
        }
        else
        {
            isInRange = false;
            interactionKey.gameObject.SetActive(false);
            interactionMessage.gameObject.SetActive(false);
        }
    }

    private void PickUpCookie()
    {
        isPickedUp = true;
        interactionKey.gameObject.SetActive(false);
        interactionMessage.gameObject.SetActive(false);
        
        // Set the cookie to be a child of the player
        transform.SetParent(player.transform);
        transform.localPosition = new Vector3(0, -0.3f, 0.5f);
        transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    private void GiveCookieToWarriorDog()
    {
        isPickedUp = false;
        interactionKey.gameObject.SetActive(false);
        interactionMessage.gameObject.SetActive(false);
        // Destroy the cookie
        Destroy(gameObject);
        // Make the warrior dog idle
        //warriorDog.GetComponent<Animator>().SetTrigger("Idle");
        // Show the hidden painting
        hiddenPainting.SetActive(true);
        // Destroy the warrior dog
        Destroy(warriorDog);
        Debug.Log("Warrior dog destroyed");
    }
}
