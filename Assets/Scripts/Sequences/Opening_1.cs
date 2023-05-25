using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Video;
using UnityStandardAssets.Characters.FirstPerson;

public class Opening_1 : MonoBehaviour
{
    public GameObject ThePlayer;
    public GameObject FadeScreenIn;
    public GameObject TextBox;
    public GameObject StartMovie;
    public bool skipped = false;
    void Start ()
	{
        //Enemy.SetActive(false);
        Time.timeScale = 0;
        ThePlayer.GetComponent<FirstPersonController>().enabled = false;
		StartCoroutine(ScenePlayer());
    }
    IEnumerator ScenePlayer ()
    {
        yield return new WaitForSeconds (111f);
        if (!skipped)
        {
            ThePlayer.GetComponent<VideoPlayer>().Stop();
            Time.timeScale = 1;
            skipped = true;
            StartCoroutine(SetupGame());
        }
    }

    public void OnSkip()
    {
        ThePlayer.GetComponent<VideoPlayer>().Stop();
        Time.timeScale = 1;
        skipped = true;
        StartCoroutine(SetupGame());
    }

    IEnumerator SetupGame()
    {
        StartMovie.SetActive(false);
        FadeScreenIn.SetActive(true);
        FadeScreenIn.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(2f);
        FadeScreenIn.SetActive(false);
        TextBox.GetComponent<Text>().text = "Congratulations! Now find the Weapon or unmask the Ghost!\nIt's up to you!";
        yield return new WaitForSeconds(5);
        TextBox.GetComponent<Text>().text = "";
        ThePlayer.GetComponent<FirstPersonController>().enabled = true;
        //Enemy.SetActive(true);
    }

}
