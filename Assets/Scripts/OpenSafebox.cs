using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;

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
                _ = EnterAsync();
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                IsCodePanelOpened = false;
                CodePanel.SetActive(false);
            }
            else if (CodeText.text.Length < 8 && Input.GetKeyDown(KeyCode.Alpha0))
            {
                AddDigit("0");
            }
            else if (CodeText.text.Length < 8 && Input.GetKeyDown(KeyCode.Alpha1))
            {
                AddDigit("1");
            }
            else if (CodeText.text.Length < 8 && Input.GetKeyDown(KeyCode.Alpha2))
            {
                AddDigit("2");
            }
            else if (CodeText.text.Length < 8 && Input.GetKeyDown(KeyCode.Alpha3))
            {
                AddDigit("3");
            }
            else if (CodeText.text.Length < 8 && Input.GetKeyDown(KeyCode.Alpha4))
            {
                AddDigit("4");
            }
            else if (CodeText.text.Length < 8 && Input.GetKeyDown(KeyCode.Alpha5))
            {
                AddDigit("5");
            }
            else if (CodeText.text.Length < 8 && Input.GetKeyDown(KeyCode.Alpha6))
            {
                AddDigit("6");
            }
            else if (CodeText.text.Length < 8 && Input.GetKeyDown(KeyCode.Alpha7))
            {
                AddDigit("7");
            }
            else if (CodeText.text.Length < 8 && Input.GetKeyDown(KeyCode.Alpha8))
            {
                AddDigit("8");
            }
            else if (CodeText.text.Length < 8 && Input.GetKeyDown(KeyCode.Alpha9))
            {
                AddDigit("9");
            }
            else if (CodeText.text.Length > 0 && Input.GetKeyDown(KeyCode.Backspace))
            {
                DeleteDigit();
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

    public void AddDigit(string digit)
    {
        CodeText.text += digit;
    }
    
    public void DeleteDigit()
    {
        CodeText.text = CodeText.text.Substring(0, CodeText.text.Length - 1);
    }

    public async Task EnterAsync()
    {
        if (CodeText.text == SafeCode)
        {
            CodeText.text = "CORRECT";
            IsWaiting = true;
            await Task.Delay(TimeSpan.FromSeconds(1f));
            IsWaiting = false;
            IsCodePanelOpened = false;
            CodePanel.SetActive(false);
            GetComponent<BoxCollider>().enabled = false;
            transform.parent.gameObject.GetComponent<Animation>().Play("OpenSafebox");
            //transform.parent.gameObject.transform.GetChild(19).transform.GetChild(1).gameObject.SetActive(true);
            KeyTrigger.SetActive(true);
        }
        else
        {
            var color = CodeText.color;
            CodeText.color = Color.red;
            CodeText.text = "WRONG";
            IsWaiting = true;
            await Task.Delay(TimeSpan.FromSeconds(1f));
            IsWaiting = false;
            CodeText.color = color;
            CodeText.text = "";
        }
    }
}
