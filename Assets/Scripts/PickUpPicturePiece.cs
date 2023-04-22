using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PickUpPicturePiece : MonoBehaviour
{
    private float TheDistance;
    public GameObject ActionDisplay;
    public GameObject ActionText;
    public GameObject ExtraCross;
    public TextMeshProUGUI CountText;
    private static int Count;

    void Start()
    {
        Count = 0;
    }

    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }

    void OnMouseOver()
    {
        if (TheDistance <= 1.8)
        {
            ExtraCross.SetActive(true);
            ActionText.GetComponent<Text>().text = "Pick up picture piece";
            ActionDisplay.GetComponent<Text>().text = "[K]";
            ActionDisplay.SetActive(true);
            ActionText.SetActive(true);
        }
        if (Input.GetButtonDown("PickUp"))
        {
            if (TheDistance <= 1.8)
            {
                GetComponent<BoxCollider>().enabled = false;
                ActionDisplay.SetActive(false);
                ActionText.SetActive(false);
                ExtraCross.SetActive(false);
                transform.parent.gameObject.SetActive(false);
                Count++;
                SetCountText();
            }
        }
    }

    void SetCountText()
    {
        if(Count == 1)
        {
            CountText.transform.parent.gameObject.SetActive(true);
        }
        CountText.text = "Count: " + Count.ToString();
        if (Count == 5)
        {
            CountText.transform.parent.gameObject.SetActive(false);
        }
    }

    void OnMouseExit()
    {
        ExtraCross.SetActive(false);
        ActionDisplay.SetActive(false);
        ActionText.SetActive(false);
    }
}
