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
    public GameObject[] RelatedObjects = new GameObject[10];

    // Start is called before the first frame update
    void Start()
    {
        CurrentPesrpsctive = "scooby";
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
            RelatedObjects[0].SetActive(false);
            RelatedObjects[1].SetActive(false);
            RelatedObjects[2].SetActive(true);
            return;
        }

        if (CurrentPesrpsctive == "shaggy")
        {
            RelatedObjects[0].SetActive(false);
            RelatedObjects[1].SetActive(false);
            RelatedObjects[2].SetActive(true);
            return;
        }

        if (CurrentPesrpsctive == "daphne")
        {
            RelatedObjects[0].SetActive(true);
            RelatedObjects[1].SetActive(false);
            RelatedObjects[2].SetActive(true);
            return;
        }

        if (CurrentPesrpsctive == "fred")
        {
            RelatedObjects[0].SetActive(false);
            RelatedObjects[1].SetActive(true);
            RelatedObjects[2].SetActive(false);
            return;
        }

        if (CurrentPesrpsctive == "velma")
        {
            RelatedObjects[0].SetActive(false);
            RelatedObjects[1].SetActive(false);
            RelatedObjects[2].SetActive(true);
            return;
        }
    }
}
