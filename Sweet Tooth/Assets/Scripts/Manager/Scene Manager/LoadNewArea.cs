using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour
{
    [Header("New area string")]
    [SerializeField] private string nextLevel;

    [Header("Determines where player spawns in next area")]
    public string exitPoint;

    private PlayerController pc;

    // Use this for initialization
    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        FindObjectOfType<CameraController>().Update_Cameras();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator GoToNextLevel()
    {
        if (pc != null)
        {
            pc.startPoint = exitPoint;
            FindObjectOfType<Fading>().FadeOut();
        }

        else
        {
            Debug.Log("No player");
        }
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(nextLevel);
        FindObjectOfType<Fading>().ResetTriggers();
        pc.transform.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        pc.transform.GetComponent<SpriteRenderer>().sortingOrder = 200;
    }

    public IEnumerator ShiftPlayerToNewSpot ()
    {
        if (pc != null)
        {
            pc.startPoint = exitPoint;
            FindObjectOfType<Fading>().FadeOut();
        }

        else
        {
            Debug.Log("No player");
        }
        yield return new WaitForSeconds(1f);

        Player_Start[] startPoints = FindObjectsOfType<Player_Start>();

        for (int i = 0; i < startPoints.Length; i++)
        {
            StartCoroutine(startPoints[i].Spawn_Player());
        }

        pc.transform.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        pc.transform.GetComponent<SpriteRenderer>().sortingOrder = 200;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (nextLevel != SceneManager.GetActiveScene().name)
            {
                StartCoroutine(GoToNextLevel());
            }

            else
            {
                StartCoroutine(ShiftPlayerToNewSpot());
            }
            //collision.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Player";
            //collision.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 200;
        }
    }
}
