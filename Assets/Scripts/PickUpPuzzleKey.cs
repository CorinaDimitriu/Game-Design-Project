using UnityEngine;
using UnityEngine.UI;

public class PickUpPuzzleKey : MonoBehaviour
{
    public float TheDistance;
    public GameObject ExtraCross;
    public GameObject ActionDisplay;
    public GameObject ActionText;
    public GameObject Key;
    public GameObject KeyTrigger;
    public GameObject inventoryManager;

    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }
    void OnMouseOver()
    {
        if (TheDistance <= 6)
        {
            ExtraCross.SetActive(true);
            ActionText.GetComponent<Text>().text = "Pick up the puzzle key";
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
                Key.SetActive(false);
                //inventoryManager.GetComponent<GeneralKeyboardActions>().enabledObjects[1] = true;
            }
        }
    }

    void OnMouseExit()
    {
        ExtraCross.SetActive(false);
        ActionDisplay.SetActive(false);
        ActionText.SetActive(false);
    }
}
