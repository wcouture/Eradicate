using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Animator anim;
    private Animator options;
    private Animator buttons;
    public GameObject animHolder;

    private void Start()
    {
        PlayerPrefs.SetFloat("AnnouncerVolume",1);
        PlayerPrefs.SetFloat("MusicVolume",1);
        PlayerPrefs.SetFloat("FXvolume", 1);
        animHolder = GameObject.Find("Cover");
        anim = animHolder.GetComponent<Animator>();
        options = GameObject.Find("Options").GetComponent<Animator>();
        buttons = GameObject.Find("Buttons").GetComponent<Animator>();
    }
    public void startGame()
    {
        StartCoroutine("LoadScene");
    }

    public void optionsMenu()
    {
        buttons.SetTrigger("buttons");
        options.SetTrigger("options");
        
    }

    public void exitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadScene()
    {
        anim.SetTrigger("SlideOut");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }
}
