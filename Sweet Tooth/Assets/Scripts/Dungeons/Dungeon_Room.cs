using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dungeon_Room : MonoBehaviour
{
    private GameObject virtualCamera;
    public GameObject mapMarker;

    private Color playerInZone;

    [SerializeField] private GameObject enemiesInRoom;

    private GameObject enemy;
    private GameObject room;

    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = gameObject.transform.GetChild(0).gameObject;

        if (gameObject.transform.GetChild(1).gameObject != null)
        {
            enemiesInRoom = gameObject.transform.GetChild(1).gameObject;
        }

        if (enemiesInRoom != null)
        {
            enemy = enemiesInRoom;
            enemiesInRoom.SetActive(false);
        }

        playerInZone = Color.green; 
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

            if (enemiesInRoom != null)
            {
                enemy = Instantiate(enemiesInRoom, transform, false);
                enemy.SetActive(true);
            }


            //mapMarker.SetActive(true);
            //mapMarker.GetComponent<Image>().color = playerInZone;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<CameraController>().Update_Cameras();
            virtualCamera.SetActive(false);

            if (enemiesInRoom != null)
            {
                Destroy(enemy);
            }
            //mapMarker.GetComponent<Image>().color = Color.white;
        }
    }
}
