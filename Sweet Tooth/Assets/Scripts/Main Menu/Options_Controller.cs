using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options_Controller : MonoBehaviour
{
    public string mainMenu;

    public GameObject audioPanel;
    public GameObject videoPanel;
    public GameObject controlsPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Deactivate_Panels ()
    {
        audioPanel.SetActive(false);
        videoPanel.SetActive(false);
        controlsPanel.SetActive(false);
    }

    public void Open_Controls_Panel ()
    {
        Deactivate_Panels();
        controlsPanel.SetActive(true);
    }

    public void Open_Video_Panel ()
    {
        Deactivate_Panels();
        videoPanel.SetActive(true);
    }

    public void Open_Audio_Panel ()
    {
        Deactivate_Panels();
        audioPanel.SetActive(true);
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
