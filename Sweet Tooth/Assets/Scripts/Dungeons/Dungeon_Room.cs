using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dungeon_Room : MonoBehaviour
{
    private GameObject virtualCamera;
    private GameObject mapMarker;

    private Color playerInZone;


    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = gameObject.transform.GetChild(0).gameObject;

        //playerInZone = Color.green; 
        //mapMarker = gameObject.transform.GetChild(1).gameObject;
        //mapMarker.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<CameraController>().Update_Cameras();
            virtualCamera.SetActive(true);

            //mapMarker.SetActive(true);
            //mapMarker.GetComponent<SpriteRenderer>().color = playerInZone;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //FindObjectOfType<CameraController>().Update_Cameras();
            //virtualCamera.SetActive(false);
            //mapMarker.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
