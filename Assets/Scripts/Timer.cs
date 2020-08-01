using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float time = 0;
    public GameObject enemyPrefab;
    private PlayerController player;
    private AudioSource begin;
    private Text displayText;
    private Animator animator;
    public AudioClip gameOver;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.Find("Cover").GetComponent<Animator>();
        displayText = GameObject.Find("TimerDisplay").GetComponent<Text>();
        begin = GetComponent<AudioSource>();
        begin.volume = PlayerPrefs.GetFloat("AnnouncerVolume");
        player = GameObject.Find("Player").GetComponentInChildren<PlayerController>();
        StartCoroutine("beginGame");
    }

    void Display()
    {
        float mins = (int)(time / 60);
        float sec = (int)(time % 60);
        if(sec < 10)
        {
            displayText.text = mins + ":0" + sec;
        }
        else
        {
            displayText.text = mins + ":" + sec;
        }
    }

    public void spawn()
    {
        float spawns = 0;
        while (spawns<(4 + (int)(time/5)))
        {
            Instantiate(enemyPrefab);
            spawns++;
        }
    }

    IEnumerator beginGame()
    {
        begin.Play();
        while (begin.isPlaying)
        {
            yield return null;
        }
        while (player.alive)
        {
            Display();
            if (time % 5 == 0)
            {
                spawn();
            }
            time += 1;
            yield return new WaitForSeconds(1);
        }
        begin.clip = gameOver;
        begin.Play();
        animator.SetTrigger("SlideOut");
        yield return new WaitForSeconds(1);
        PlayerPrefs.SetFloat("RecentTime",time);
        SceneManager.LoadScene(2);
    }
}
