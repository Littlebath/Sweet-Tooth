using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MainMenu : MonoBehaviour
{
    public string StartGameScene;
    public string OptionsScene;


    // Use this for initialization
    void Start()
    {
        
    }

	// Update is called once per frame
	void Update ()
    {
		
	}


    public void LoadScene (string name)
    {

    }

    public void StartTheGame ()
    {
        StartCoroutine(TrueStart());
    }

    public void Options ()
    {
        StartCoroutine(TrueOptions());
    }

    private IEnumerator TrueStart ()
    {
        FindObjectOfType<Fading>().FadeOut();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(StartGameScene);
    }

    private IEnumerator TrueOptions ()
    {
        FindObjectOfType<Fading>().FadeOut();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(OptionsScene);
    }

    public void ExitGame ()
    {
        Application.Quit();
    }


}

