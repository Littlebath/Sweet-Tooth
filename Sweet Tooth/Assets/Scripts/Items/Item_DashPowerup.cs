using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_DashPowerup : MonoBehaviour
{
    [SerializeField] private Player_ScriptableObject values;

    bool pickedUp;

    private Sprite originalSprite;

    // Start is called before the first frame update
    void Start()
    {
        originalSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (pickedUp)
        {
            if (FindObjectOfType<PlayerInput>().interactButton)
            {
                FindObjectOfType<Manager_Dialogue>().DisplayNextSentence();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            values.hasDash = true;
            StartCoroutine(PickUp_Animation());
        }
    }

    IEnumerator PickUp_Animation()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
        FindObjectOfType<PlayerInput>().transform.GetChild(4).GetComponent<SpriteRenderer>().sprite = originalSprite;
        FindObjectOfType<PlayerController>().GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        FindObjectOfType<PlayerController>().enabled = false;
        FindObjectOfType<PlayerInput>().GetComponent<Animator>().SetBool("hasItem", true);
        yield return new WaitForSeconds(2f);
        FindObjectOfType<PlayerController>().GetComponent<Animator>().SetBool("isMoving", false);
        FindObjectOfType<PlayerInput>().GetComponent<Animator>().SetBool("hasItem", false);
        FindObjectOfType<PlayerInput>().transform.GetChild(4).GetComponent<SpriteRenderer>().sprite = null;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<Dialogue_Trigger>().TriggerDialogue();
        pickedUp = true;
    }
}
