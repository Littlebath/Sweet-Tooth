using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    public string Menu;

    private static bool isInventoryExsisting;

    // Use this for initialization
    void Start ()
    {
        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            DestroyDuplicates();
        }

        else
        {
            Debug.Log ("Please Destroy");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }

            else
            {
                Pause();
            }
        }
	}

    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void TrueLoadMenu ()
    {
        StartCoroutine(LoadMenu());
    }

    public IEnumerator LoadMenu ()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        FindObjectOfType<Fading>().FadeOut();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(Menu);
    }

    public void QuitGame ()
    {
        Application.Quit();
    }

    private void DestroyDuplicates()
    {
        if (!isInventoryExsisting)
        {
            isInventoryExsisting = true;
            DontDestroyOnLoad(transform.gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

    }
}
