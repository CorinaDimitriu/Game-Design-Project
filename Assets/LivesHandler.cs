using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesHandler : MonoBehaviour
{
    public int maxLives;
    public int currentLives;

    private Image[] lifeImages;

    // Start is called before the first frame update
    void Start()
    {
        lifeImages = GetComponentsInChildren<Image>();
        UpdateLives();
    }

    // Update is called once per frame
    void UpdateLives()
    {
        for (int i = 0; i < maxLives; i++)
        {
            if (i < currentLives)
            {
                lifeImages[i].enabled = true;
            }
            else
            {
                lifeImages[i].enabled = false;
            }
        }
    }
}
