using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_PlayLevelTheme : MonoBehaviour
{
    public string levelSong;

    public bool isPlayerLightOn;
    // Start is called before the first frame update
    void Start ()
    {
        StartCoroutine(PlaySongAtStart());

        /*if (gameObject.GetComponent<Dialogue_Trigger>() != null)
        {
            gameObject.GetComponent<Dialogue_Trigger>().TriggerDialogue();
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<PlayerController>() != null)
        {
            if (isPlayerLightOn)
            {
                FindObjectOfType<PlayerController>().transform.GetChild(0).gameObject.SetActive(true);
            }

            else
            {
                FindObjectOfType<PlayerController>().transform.GetChild(0).gameObject.SetActive(false);
            }
        }

    }

    IEnumerator PlaySongAtStart ()
    {
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<Manager_AudioManager>().Play(levelSong);
        //Debug.Log(levelSong);
    }
}
