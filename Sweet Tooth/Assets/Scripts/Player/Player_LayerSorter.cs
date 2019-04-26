using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_LayerSorter : MonoBehaviour
{
    private string originalSortingLayer;

    [SerializeField] private Collider2D currentCollider;

    // Start is called before the first frame update
    void Start()
    {
        originalSortingLayer = transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName;

        if (SceneManager.GetActiveScene().name == "Area 2")
        {
            transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Ground";
            transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCollider == null)
        {
            transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = originalSortingLayer;
            transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 200;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag ("NPC") || collision.gameObject.CompareTag("Checkpoint") || collision.gameObject.CompareTag("EnemyLayer") || collision.gameObject.CompareTag("breakable"))
        {
            transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = collision.transform.parent.GetComponent<SpriteRenderer>().sortingLayerName;
            transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = collision.transform.parent.GetComponent<SpriteRenderer>().sortingOrder - 1;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "PlayerBehind";
            transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -200;
        }

        currentCollider = collision;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC") || collision.gameObject.CompareTag("Checkpoint") || collision.gameObject.CompareTag("EnemyLayer") || collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("breakable"))
        {
            if (SceneManager.GetActiveScene().name != "Area 2")
            {
                transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = originalSortingLayer;
                transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 200;
            }

            else
            {
                transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Ground";
                transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 1;
            }

        }
    }
}
