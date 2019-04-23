using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Map : MonoBehaviour
{
    private PlayerController pc;
    private PlayerInput pi;
    private GameObject map;

    [HideInInspector] public bool isMapOn;
    // Start is called before the first frame update
    void Start()
    {
        pi = FindObjectOfType<PlayerInput>();
        pc = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        MapControl();
    }

    private void MapControl()
    {
        if (pi.mapButton)
        {
            map = GameObject.FindGameObjectWithTag("Map").transform.GetChild(0).gameObject;

            if (isMapOn)
            {
                StartCoroutine(CloseMap());
            }

            else
            {
                StartCoroutine(OpenMap());
            }
        }
    }

    private IEnumerator CloseMap()
    {
        isMapOn = false;
        map.SetActive(false);
        pc.enabled = true;
        Debug.Log("Close map");
        yield return null;
    }

    private IEnumerator OpenMap()
    {
        isMapOn = true;
        pc.enabled = false;
        FindObjectOfType<PlayerInput>().GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        map.SetActive(true);
        Debug.Log("Open map");
        yield return null;
    }

}
