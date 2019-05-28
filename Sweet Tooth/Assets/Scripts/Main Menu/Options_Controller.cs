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
    public GameObject audioPanel;
    public GameObject videoPanel;
    public GameObject controlsPanel;

    [Header("Settings Menu Variables")]
    public AudioMixer audioMixer;
    public AudioMixerGroup effect;

    Resolution[] resolutions;

    [Header("Controls variables")]
    public GameObject keyboardPanel;
    public GameObject controllerPanel;


    private void Start()
    {
        Deactivate_Panels();

        resolutions = Screen.resolutions;

        //resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Master Volume", volume);
    }

    public void SetEffect(float volume)
    {
        effect.audioMixer.SetFloat("Effect Volume", volume);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    private void Update()
    {
        if (controlsPanel.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Keyboard panel open");
                Open_Keyboard();

            }

            else if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Controller panel open");
                Open_Controller();
            }
        }
    }

    private void Open_Keyboard ()
    {
        //Deactivate_Panels();
        controllerPanel.SetActive(false);
        keyboardPanel.SetActive(true);
    }

    private void Open_Controller ()
    {
        //Deactivate_Panels();
        keyboardPanel.SetActive(false);
        controllerPanel.SetActive(true);
    }

    private void Deactivate_Panels ()
    {
        audioPanel.SetActive(false);
        videoPanel.SetActive(false);
        controlsPanel.SetActive(false);
        controllerPanel.SetActive(false);
        keyboardPanel.SetActive(false);
    }

    public void Open_Controls_Panel ()
    {
        Deactivate_Panels();
        controlsPanel.SetActive(true);
        keyboardPanel.SetActive(true);
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
