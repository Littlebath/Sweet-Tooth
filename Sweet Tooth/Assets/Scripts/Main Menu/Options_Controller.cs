using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options_Controller : MonoBehaviour
{
    public string mainMenu;

    [Header("Option controller variables")]
    public GameObject gamepadPanel;
    public GameObject keyboardPanel;

    [Header("Buttons")]
    public GameObject gamepadButton;
    public GameObject keyboardButton;



    private void Start()
    {
        Deactivate_Panels();
        Open_Keyboard();
    }

    public void Open_Keyboard ()
    {
        Deactivate_Panels();
        gamepadPanel.SetActive(false);
        keyboardPanel.SetActive(true);
    }

    public void Open_Controller ()
    {
        Deactivate_Panels();
        keyboardPanel.SetActive(false);
        gamepadPanel.SetActive(true);
    }

    void Deactivate_Panels ()
    {
        keyboardPanel.SetActive(false);
        gamepadPanel.SetActive(false);
    }

    public void MainMenu ()
    {
        StartCoroutine(Run_MainMenu());
    }

    private IEnumerator Run_MainMenu ()
    {
        FindObjectOfType<Fading>().FadeOut();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(mainMenu);
    }
}
