using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private Text timeDisplay;
    private Text bestTime;
    private Animator anim;
    private void Awake()
    {
        bestTime = GameObject.Find("BestTimeDisplay").GetComponent<Text>();
        timeDisplay = GameObject.Find("TimeDisplay").GetComponent<Text>();
        displayTime();
        checkBestTime();
        displayBestTime();
    }

    void displayTime()
    {
        float mins = (int)(PlayerPrefs.GetFloat("RecentTime") / 60);
        float sec = (int)(PlayerPrefs.GetFloat("RecentTime") % 60);
        if(sec < 10)
        {
            timeDisplay.text = "Time: " + mins + ":0" + sec;
        }
        else
        {
            timeDisplay.text = "Time: " + mins + ":" + sec;
        }
    }

    void checkBestTime()
    {
        if(PlayerPrefs.GetFloat("RecentTime") > PlayerPrefs.GetFloat("BestTime"))
        {
            PlayerPrefs.SetFloat("BestTime",PlayerPrefs.GetFloat("RecentTime"));
        }
    }

    void displayBestTime()
    {
        float mins = (int)(PlayerPrefs.GetFloat("BestTime") / 60);
        float sec = (int)(PlayerPrefs.GetFloat("BestTime") % 60);
        if(sec < 10)
        {
            bestTime.text = "Best Time: " + mins + ":0" + sec;
        }
        else
        {
            bestTime.text = "Best Time: " + mins + ":" + sec;
        }
    }

    public void quit()
    {
        Application.Quit();
    }

    public void restartGame()
    {
        StartCoroutine("restart");
    }

    IEnumerator restart()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

}
