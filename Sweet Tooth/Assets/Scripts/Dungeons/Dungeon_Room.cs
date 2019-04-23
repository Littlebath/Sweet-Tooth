using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dungeon_Room : MonoBehaviour
{
    private GameObject virtualCamera;
    [SerializeField] private GameObject blackCover;
    [SerializeField] private GameObject inLightMap;

    private Color mapColor;

    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = gameObject.transform.GetChild(0).gameObject;
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
            blackCover.SetActive(false);
            mapColor = Color.white;
            inLightMap.GetComponent<Image>().color = mapColor;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //FindObjectOfType<CameraController>().Update_Cameras();
            virtualCamera.SetActive(false);
            mapColor = Color.grey;
            inLightMap.GetComponent<Image>().color = mapColor;
        }
    }
}
