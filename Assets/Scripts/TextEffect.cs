using System.Collections;
using UnityEngine;

public class TextEffect : MonoBehaviour
{
    private int[] Flag = new int[9];
    private int Child;
    private int Turn;

    void Start()
    {
        transform.GetChild(0).Find("Text_family").GetComponent<CanvasGroup>().alpha = 0.025f;
        transform.GetChild(0).Find("Text_is").GetComponent<CanvasGroup>().alpha = 0.025f;
        transform.GetChild(0).Find("Text_the").GetComponent<CanvasGroup>().alpha = 0.025f;
        transform.GetChild(0).Find("Text_most").GetComponent<CanvasGroup>().alpha = 0.025f;
        transform.GetChild(0).Find("Text_important").GetComponent<CanvasGroup>().alpha = 0.025f;
        transform.GetChild(1).Find("Text_thing").GetComponent<CanvasGroup>().alpha = 0.025f;
        transform.GetChild(1).Find("Text_in").GetComponent<CanvasGroup>().alpha = 0.025f;
        transform.GetChild(1).Find("Text_the2").GetComponent<CanvasGroup>().alpha = 0.025f;
        transform.GetChild(1).Find("Text_world").GetComponent<CanvasGroup>().alpha = 0.025f;
        for (int i = 0; i < 9; i++)
            Flag[i] = 1;
        Child = 0;
        Turn = 0;
    }

    void Update()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        string text = "";
        switch (Turn)
        {
            case 0: text = "family"; break;
            case 1: text = "is"; break;
            case 2: text = "the"; break;
            case 3: text = "most"; break;
            case 4: text = "important"; break;
            case 5: text = "thing"; break;
            case 6: text = "in"; break;
            case 7: text = "the2"; break;
            case 8: text = "world"; break;
        }
        CanvasGroup word = transform.GetChild(Child).Find("Text_" + text).GetComponent<CanvasGroup>();
        if (word.alpha == 1f)
        {
            Flag[Turn] = -1;
        }
        word.alpha += Flag[Turn] * 0.025f;
        if (word.alpha == 0f)
        {
            Flag[Turn] = 1;
            Turn = (Turn + 1) % 9;
            if(Turn > 4)
            {
                Child = 1;
            }
            else
            {
                Child = 0;
            }
        }
        yield return new WaitForSeconds(2f);
    }
}
