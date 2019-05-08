using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_PlayLevelTheme : MonoBehaviour
{
    public string levelSong;
    // Start is called before the first frame update
    void Start ()
    {
        StartCoroutine(PlaySongAtStart());

        if (gameObject.GetComponent<Dialogue_Trigger>() != null)
        {
            gameObject.GetComponent<Dialogue_Trigger>().TriggerDialogue();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Dialogue_Trigger>() != null)
        {
            if (FindObjectOfType<PlayerInput>().interactButton)
            {
                if (!FindObjectOfType<Manager_Dialogue>().isTalking)
                {
                    FindObjectOfType<PlayerController>().isMelee = false;
                    Debug.Log("Start dialogue");
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    //Debug.Log("Talk");
                }

                else
                {
                    FindObjectOfType<Manager_Dialogue>().DisplayNextSentence();
                }
            }
        }
    }

    IEnumerator PlaySongAtStart ()
    {
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<Manager_AudioManager>().Play(levelSong);
        Debug.Log(levelSong);
    }
}
