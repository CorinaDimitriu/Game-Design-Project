using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class OpenSafebox : MonoBehaviour
{
    public GameObject ActionDisplay;
    public GameObject ActionText;
    public GameObject ExtraCross;
    public TextMeshProUGUI CodeText;
    public string SafeCode;
    public GameObject CodePanel;
    public GameObject KeyTrigger;
    private float TheDistance;
    private bool IsCodePanelOpened = false;
    private bool IsWaiting = false;

    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
        if (IsCodePanelOpened && !IsWaiting)
        {
            if (CodeText.text.Length == 8 || Input.GetKeyDown(KeyCode.Return))
            {
                StartCoroutine(Enter());
                StartCoroutine(ColorButton("_ok"));
                StopCoroutine(ColorButton("_ok"));
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                IsCodePanelOpened = false;
                CodePanel.SetActive(false);
            }
            else if (CodeText.text.Length < 8 && Input.GetKeyDown(KeyCode.Alpha0))
            {
                AddDigit("0");
                StartCoroutine(ColorButton(" (0)"));
                StopCoroutine(ColorButton(" (0)"));
            }
            else if (CodeText.text.Length < 8 && Input.GetKeyDown(KeyCode.Alpha1))
            {
                AddDigit("1");
                StartCoroutine(ColorButton(" (1)"));
                StopCoroutine(ColorButton(" (1)"));
            }
            else if (CodeText.text.Length < 8 && Input.GetKeyDown(KeyCode.Alpha2))
            {
                AddDigit("2");
                StartCoroutine(ColorButton(" (2)"));
                StopCoroutine(ColorButton(" (2)"));
            }
            else if (CodeText.text.Length < 8 && Input.GetKeyDown(KeyCode.Alpha3))
            {
                AddDigit("3");
                StartCoroutine(ColorButton(" (3)"));
                StopCoroutine(ColorButton(" (3)"));

            }
            else if (CodeText.text.Length < 8 && Input.GetKeyDown(KeyCode.Alpha4))
            {
                AddDigit("4");
                StartCoroutine(ColorButton(" (4)"));
                StopCoroutine(ColorButton(" (4)"));
            }
            else if (CodeText.text.Length < 8 && Input.GetKeyDown(KeyCode.Alpha5))
            {
                AddDigit("5");
                StartCoroutine(ColorButton(" (5)"));
                StopCoroutine(ColorButton(" (5)"));
            }
            else if (CodeText.text.Length < 8 && Input.GetKeyDown(KeyCode.Alpha6))
            {
                AddDigit("6");
                StartCoroutine(ColorButton(" (6)"));
                StopCoroutine(ColorButton(" (6)"));
            }
            else if (CodeText.text.Length < 8 && Input.GetKeyDown(KeyCode.Alpha7))
            {
                AddDigit("7");
                StartCoroutine(ColorButton(" (7)"));
                StopCoroutine(ColorButton(" (7)"));
            }
            else if (CodeText.text.Length < 8 && Input.GetKeyDown(KeyCode.Alpha8))
            {
                AddDigit("8");
                StartCoroutine(ColorButton(" (8)"));
                StopCoroutine(ColorButton(" (8)"));
            }
            else if (CodeText.text.Length < 8 && Input.GetKeyDown(KeyCode.Alpha9))
            {
                AddDigit("9");
                StartCoroutine(ColorButton(" (9)"));
                StopCoroutine(ColorButton(" (9)"));
            }
            else if (CodeText.text.Length > 0 && Input.GetKeyDown(KeyCode.Backspace))
            {
                DeleteDigit();
                StartCoroutine(ColorButton("_delete"));
                StopCoroutine(ColorButton("_delete"));
            }
        }
    }

    void OnMouseOver()
    {
        if (TheDistance <= 1.8)
        {
            ExtraCross.SetActive(true);
            ActionText.GetComponent<Text>().text = "Open the safe";
            ActionDisplay.GetComponent<Text>().text = "[E]";
            ActionDisplay.SetActive(true);
            ActionText.SetActive(true);
        }
        if (Input.GetButtonDown("Action"))
        {
            if (TheDistance <= 1.8)
            {
                ActionDisplay.SetActive(false);
                ActionText.SetActive(false);
                ExtraCross.SetActive(false);
                CodePanel.SetActive(true);
                IsCodePanelOpened = true;
            }
        }
    }

    void OnMouseExit()
    {
        ExtraCross.SetActive(false);
        ActionDisplay.SetActive(false);
        ActionText.SetActive(false);
    }

    IEnumerator ColorButton(string label)
    {
        Image button = CodePanel.transform.Find("Button" + label).GetComponent<Image>();
        if (label == "_delete")
        {
            button.color = new Color32(111, 0, 0, 255);
        }
        else
        {
            button.color = new Color32(0, 91, 10, 255);
        }
        yield return new WaitForSeconds(0.05f);
        button.color = new Color32(255, 255, 255, 255);
    }

    public void AddDigit(string digit)
    {
        CodeText.text += digit;
    }
    
    public void DeleteDigit()
    {
        CodeText.text = CodeText.text.Substring(0, CodeText.text.Length - 1);
    }

    IEnumerator CorrectMessage()
    {
        CodeText.text = "CORRECT";
        yield return new WaitForSeconds(1f);
    }
    
    IEnumerator WrongMessage()
    {
        CodeText.text = "WRONG";
        var color = CodeText.color;
        CodeText.color = Color.red;
        yield return new WaitForSeconds(1f);
        CodeText.color = color;
        CodeText.text = "";
    }

    public IEnumerator Enter()
    {
        if (CodeText.text == SafeCode)
        {
            IsWaiting = true;
            yield return StartCoroutine(CorrectMessage());
            StopCoroutine(CorrectMessage());
            IsWaiting = false;
            IsCodePanelOpened = false;
            CodePanel.SetActive(false);
            GetComponent<BoxCollider>().enabled = false;
            transform.parent.gameObject.GetComponent<Animation>().Play("OpenSafebox");
            KeyTrigger.SetActive(true);
        }
        else
        {
            IsWaiting = true;
            yield return StartCoroutine(WrongMessage());
            StopCoroutine(WrongMessage());
            IsWaiting = false;
        }
    }

    public void EnterOk()
    {
        StartCoroutine(Enter());
    }
}
