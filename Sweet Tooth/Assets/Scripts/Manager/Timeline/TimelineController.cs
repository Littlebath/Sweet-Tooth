using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public PlayableAsset scene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<Manager_Timeline>().GetComponent<PlayableDirector>().playableAsset = scene;
            FindObjectOfType<Manager_Timeline>().GetComponent<PlayableDirector>().Play();
            gameObject.SetActive(false);
        }
    }
}
