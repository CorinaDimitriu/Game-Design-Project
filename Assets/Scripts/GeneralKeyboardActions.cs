using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class GeneralKeyboardActions : MonoBehaviour
{
    public GameObject inventory;
    public TMP_Text page;
    private Sprite un_highlighted;
    private Sprite highlighted;
    private int current;
    public bool[] enabledObjects = new bool[10];
    private readonly int noObjects = 6;
    private Button[] buttons = new Button[10];
    private int prevCount;
    public GameObject[] RealObjects = new GameObject[10];

    // Start is called before the first frame update
    void Start()
    {
        current = 1;
        prevCount = 0;
        for (int i = 0; i < 10; i++)
            enabledObjects[i] = false;
        enabledObjects[5] = true;
        enabledObjects[6] = true;
        for (int button = 1; button <= noObjects; button++)
        {
            buttons[button] = GameObject.Find("Object" + button.ToString()).GetComponent<Button>();
            if (button > 3)
                buttons[button].gameObject.SetActive(false);
        }
        inventory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        int newlyAdded = enabledObjects.Count(ob => ob == true);
        if (Math.Abs(newlyAdded - prevCount) > 0)
        {
            prevCount = newlyAdded;
            ChangeSprites();
        }

        if (Input.GetButtonDown("OpenCloseInventory"))
        {
            if (inventory.activeSelf == false)
            {
                inventory.SetActive(true);
            }
            else
            {
                inventory.SetActive(false);
            }
        }

        if (inventory.activeSelf == true && Input.GetButtonDown("LeftArrow") && current > 1)
        {
            Button currentButton = buttons[current];
            if (enabledObjects[current])
            {
                un_highlighted = Resources.Load<Sprite>("object" + current.ToString());
                currentButton.GetComponent<Image>().sprite = un_highlighted;
            }
            else
            {
                un_highlighted = Resources.Load<Sprite>("transparent");
                currentButton.GetComponent<Image>().sprite = un_highlighted;
            }
            current--;
            if(current % 3 == 0)
            {
                page.text = (current / 3).ToString() + "/" + (Math.Ceiling(noObjects / 3.0)).ToString();
                currentButton.enabled = false;
                for(int previous = current + 2; previous <= current + 3; previous++)
                {
                    buttons[previous].gameObject.SetActive(false);
                }
                for(int next = current; next >= current - 2; next--)
                {
                    buttons[next].gameObject.SetActive(true);
                }
            }
            Button nextButton = buttons[current];
            if (enabledObjects[current])
            {
                highlighted = Resources.Load<Sprite>("object" + current.ToString() + "_highlighted");
                nextButton.GetComponent<Image>().sprite = highlighted;
            }
            else
            {
                highlighted = Resources.Load<Sprite>("empty");
                nextButton.GetComponent<Image>().sprite = highlighted;
            }

        }

        if (inventory.activeSelf == true && Input.GetButtonDown("RightArrow") && current < noObjects)
        {
            Button currentButton = buttons[current];
            if (enabledObjects[current])
            {
                un_highlighted = Resources.Load<Sprite>("object" + current.ToString());
                currentButton.GetComponent<Image>().sprite = un_highlighted;
            }
            else
            {
                un_highlighted = Resources.Load<Sprite>("transparent");
                currentButton.GetComponent<Image>().sprite = un_highlighted;
            }
            current++;
            if (current % 3 == 1)
            {
                page.text = (current / 3 + 1).ToString() + "/" + (Math.Ceiling(noObjects / 3.0)).ToString();
                currentButton.enabled = false;
                for (int previous = current - 2; previous >= current - 3; previous--)
                {
                    buttons[previous].gameObject.SetActive(false);
                }
                for (int next = current; next <= current + 2; next++)
                {
                    buttons[next].gameObject.SetActive(true);
                }
            }
            Button nextButton = buttons[current];
            if (enabledObjects[current])
            {
                highlighted = Resources.Load<Sprite>("object" + current.ToString() + "_highlighted");
                nextButton.GetComponent<Image>().sprite = highlighted;
            }
            else
            {
                highlighted = Resources.Load<Sprite>("empty");
                nextButton.GetComponent<Image>().sprite = highlighted;
            }
        }

        if(inventory.activeSelf == true && Input.GetButtonDown("Take")
            && enabledObjects[current] == true)
            {
                if (RealObjects[current].activeSelf == false)
                {
                    RealObjects[current].SetActive(true);
                }
                else
                {
                    RealObjects[current].SetActive(false);
                }
            }
    }

    void ChangeSprites()
    {
        for (int ob = 1; ob <= noObjects; ob++)
            if (enabledObjects[ob] == true)
            {
                if (current == ob)
                {
                    buttons[ob].GetComponent<Image>().sprite =
                        Resources.Load<Sprite>("object" + current.ToString() + "_highlighted");
                }
                else
                {
                    buttons[ob].GetComponent<Image>().sprite =
                        Resources.Load<Sprite>("object" + ob.ToString());
                }
            }
            else
            {
                if (current == ob)
                {
                    buttons[ob].GetComponent<Image>().sprite =
                        Resources.Load<Sprite>("empty");
                }
                else
                {
                    buttons[ob].GetComponent<Image>().sprite =
                        Resources.Load<Sprite>("transparent");
                }
            }
    }
}
