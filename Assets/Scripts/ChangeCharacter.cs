using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCharacter : MonoBehaviour
{
    public GameObject characterMenu;
    private Sprite un_highlighted;
    private Sprite highlighted;
    public string CurrentPesrpsctive;
    public GameObject[] RelatedObjects = new GameObject[12];
    [SerializeField]
    private GameObject SafeboxTrigger;

    // For paintings room
    public Material englishMessageMarble;
    public Material greekMessageMarble;
    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        CurrentPesrpsctive = "scooby";
        // Get the mesh renderer from the marble plaque
        meshRenderer = RelatedObjects[11].GetComponent<MeshRenderer>();
        ApplyChanges();
    }

    string NextCharacter(string currentCharacter)
    {
        switch(currentCharacter)
        {
            case "scooby": return "shaggy";
            case "shaggy": return "daphne";
            case "daphne": return "fred";
            case "fred": return "velma";
            case "velma": return "scooby";
            default: return "scooby";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("OpenCloseCharacterMenu"))
        {
            if (characterMenu.activeSelf == false)
            {
                characterMenu.SetActive(true);
            }
            else
            {
                characterMenu.SetActive(false);
            }
        }

        if (characterMenu.activeSelf == true && Input.GetButtonDown("TabArrow"))
        {
            Button currentButton = GameObject.Find(CurrentPesrpsctive).GetComponent<Button>();
            
            un_highlighted = Resources.Load<Sprite>(CurrentPesrpsctive);
            currentButton.GetComponent<Image>().sprite = un_highlighted;
            CurrentPesrpsctive = NextCharacter(CurrentPesrpsctive);
     
            Button nextButton = GameObject.Find(CurrentPesrpsctive).GetComponent<Button>();
            highlighted = Resources.Load<Sprite>(CurrentPesrpsctive + "_highlighted");
            nextButton.GetComponent<Image>().sprite = highlighted;

            ApplyChanges();
        }
    }

    void ApplyChanges()
    {
        if(CurrentPesrpsctive == "scooby")
        {
            RelatedObjects[0].SetActive(false); //mirror
            RelatedObjects[1].SetActive(false); //mannequin
            RelatedObjects[2].SetActive(true); //ghost mesh
            RelatedObjects[3].SetActive(false); //quotes
            RelatedObjects[4].SetActive(false); //safebox
            RelatedObjects[5].SetActive(true); //piece1
            RelatedObjects[6].SetActive(true); //piece2
            RelatedObjects[7].SetActive(true); //piece3
            RelatedObjects[8].SetActive(true); //piece4
            RelatedObjects[9].SetActive(true); //piece5
            RelatedObjects[10].SetActive(false); //code panel
            // For the paintings room
            meshRenderer.material = greekMessageMarble;

            if (SafeboxTrigger.GetComponent<OpenSafebox>().IsTextPrinted)
            {
                SafeboxTrigger.GetComponent<OpenSafebox>().ChangedCharacter();
            }
            return;
        }

        if (CurrentPesrpsctive == "shaggy")
        {
            RelatedObjects[0].SetActive(false);
            RelatedObjects[1].SetActive(false);
            RelatedObjects[2].SetActive(true);
            RelatedObjects[3].SetActive(false);
            RelatedObjects[4].SetActive(false);
            RelatedObjects[5].SetActive(false);
            RelatedObjects[6].SetActive(false);
            RelatedObjects[7].SetActive(false);
            RelatedObjects[8].SetActive(false);
            RelatedObjects[9].SetActive(false);
            RelatedObjects[10].SetActive(false);
            // For the paintings room
            meshRenderer.material = greekMessageMarble;

            if (SafeboxTrigger.GetComponent<OpenSafebox>().IsTextPrinted)
            {
                SafeboxTrigger.GetComponent<OpenSafebox>().ChangedCharacter();
            }
            return;
        }

        if (CurrentPesrpsctive == "daphne")
        {
            RelatedObjects[0].SetActive(true);
            RelatedObjects[1].SetActive(false);
            RelatedObjects[2].SetActive(true);
            RelatedObjects[3].SetActive(false);
            RelatedObjects[4].SetActive(false);
            RelatedObjects[5].SetActive(false);
            RelatedObjects[6].SetActive(false);
            RelatedObjects[7].SetActive(false);
            RelatedObjects[8].SetActive(false);
            RelatedObjects[9].SetActive(false);
            RelatedObjects[10].SetActive(false);
            // For the paintings room
            meshRenderer.material = greekMessageMarble;

            if (SafeboxTrigger.GetComponent<OpenSafebox>().IsTextPrinted)
            {
                SafeboxTrigger.GetComponent<OpenSafebox>().ChangedCharacter();
            }
            return;
        }

        if (CurrentPesrpsctive == "fred")
        {
            RelatedObjects[0].SetActive(false);
            RelatedObjects[1].SetActive(true);
            RelatedObjects[2].SetActive(false);
            RelatedObjects[3].SetActive(true);
            RelatedObjects[4].SetActive(false);
            RelatedObjects[5].SetActive(false);
            RelatedObjects[6].SetActive(false);
            RelatedObjects[7].SetActive(false);
            RelatedObjects[8].SetActive(false);
            RelatedObjects[9].SetActive(false);
            RelatedObjects[10].SetActive(false);
            // For the paintings room
            meshRenderer.material = greekMessageMarble;

            if (SafeboxTrigger.GetComponent<OpenSafebox>().IsTextPrinted)
            {
                SafeboxTrigger.GetComponent<OpenSafebox>().ChangedCharacter();
            }
            return;
        }

        if (CurrentPesrpsctive == "velma")
        {
            RelatedObjects[0].SetActive(false);
            RelatedObjects[1].SetActive(false);
            RelatedObjects[2].SetActive(true);
            RelatedObjects[3].SetActive(false);
            RelatedObjects[4].SetActive(true);
            RelatedObjects[5].SetActive(false);
            RelatedObjects[6].SetActive(false);
            RelatedObjects[7].SetActive(false);
            RelatedObjects[8].SetActive(false);
            RelatedObjects[9].SetActive(false);            
            // For the paintings room
            meshRenderer.material = englishMessageMarble;

            if (SafeboxTrigger.GetComponent<OpenSafebox>().GetIsCodePanelOpened())
            {
                RelatedObjects[10].SetActive(true);
            }
            else
            {
                RelatedObjects[10].SetActive(false);
            }
            return;
        }
    }
}
