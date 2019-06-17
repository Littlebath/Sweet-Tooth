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
            if (gameObject.GetComponent<Save_ObjState>().obj.saveState == 0)
            {
                FindObjectOfType<Manager_Timeline>().GetComponent<PlayableDirector>().playableAsset = scene;
                FindObjectOfType<Manager_Timeline>().GetComponent<PlayableDirector>().Play();
                gameObject.GetComponent<Save_ObjState>().obj.saveState = 1;
                gameObject.GetComponent<Save_ObjState>().obj.ForceSerialization();
                gameObject.SetActive(false);
            }

        }
    }
}
