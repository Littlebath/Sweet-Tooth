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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlaySongAtStart ()
    {
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<Manager_AudioManager>().Play(levelSong);
        Debug.Log(levelSong);
    }
}
