using System.Collections;
using UnityEngine;

public class TextEffect : MonoBehaviour
{
    private int[] flag = new int[9];
    private int child;
    private int turn;

    void Start()
    {
        transform.GetChild(0).Find("Text_family").GetComponent<CanvasGroup>().alpha = 0.01f;
        transform.GetChild(0).Find("Text_is").GetComponent<CanvasGroup>().alpha = 0.01f;
        transform.GetChild(0).Find("Text_the").GetComponent<CanvasGroup>().alpha = 0.01f;
        transform.GetChild(0).Find("Text_most").GetComponent<CanvasGroup>().alpha = 0.01f;
        transform.GetChild(0).Find("Text_important").GetComponent<CanvasGroup>().alpha = 0.01f;
        transform.GetChild(1).Find("Text_thing").GetComponent<CanvasGroup>().alpha = 0.01f;
        transform.GetChild(1).Find("Text_in").GetComponent<CanvasGroup>().alpha = 0.01f;
        transform.GetChild(1).Find("Text_the2").GetComponent<CanvasGroup>().alpha = 0.01f;
        transform.GetChild(1).Find("Text_world").GetComponent<CanvasGroup>().alpha = 0.01f;
        for (int i = 0; i < 9; i++)
            flag[i] = 1;
        child = 0;
        turn = 0;
    }

    void Update()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        string text = "";
        switch (turn)
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
        CanvasGroup word = transform.GetChild(child).Find("Text_" + text).GetComponent<CanvasGroup>();
        if (word.alpha == 1f)
        {
            flag[turn] = -1;
        }
        word.alpha += flag[turn] * 0.01f;
        if (word.alpha == 0f)
        {
            flag[turn] = 1;
            turn = (turn + 1) % 9;
            if(turn > 4)
            {
                child = 1;
            }
            else
            {
                child = 0;
            }
        }
        yield return new WaitForSeconds(5f);
    }
}
