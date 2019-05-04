﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfBoulder
{
    Random,
    Player
}


public class Boss_Boulder : MonoBehaviour
{
    public TypeOfBoulder boulderType;

    [SerializeField] private OreoBossScriptableObject values;

    private Boss_OreoChocolateBoss boulderProperties;
    float xPosition;
    float yPosition;

    Vector2 boulderLandPos;

    // Start is called before the first frame update
    void Start()
    {
        Boulder_Start_Position();
        Boulder_Trajectory();
    }

    void Boulder_Start_Position ()
    {
        boulderProperties = FindObjectOfType<Boss_OreoChocolateBoss>();

        yPosition = boulderProperties.spawnHeight.transform.position.y;

        if (boulderType == TypeOfBoulder.Player)
        {
            xPosition = FindObjectOfType<PlayerController>().transform.position.x;
        }

        else if (boulderType == TypeOfBoulder.Random)
        {
            xPosition = Random.Range(boulderProperties.minX.transform.position.x, boulderProperties.maxX.transform.position.x);
        }

        transform.position = new Vector2(xPosition, yPosition);
    }

    void Boulder_Trajectory ()
    {
        if (boulderType == TypeOfBoulder.Player)
        {
            boulderLandPos = FindObjectOfType<PlayerController>().transform.position;
        }

        else if (boulderType == TypeOfBoulder.Random)
        {
            boulderLandPos = new Vector2(xPosition, Random.Range(boulderProperties.minX.transform.position.y, boulderProperties.maxX.transform.position.y));
        }

        Debug.Log(boulderLandPos);
    }

    void Boulder_Falls ()
    {
        if (Vector3.Distance (transform.position, boulderLandPos) <= 0.3f)
        {
            Destroy(gameObject);
        }

        else
        {
            Vector3 tempPos = Vector3.MoveTowards(transform.position, boulderLandPos, values.boulderFallSpeed);
            gameObject.GetComponent<Rigidbody2D>().MovePosition(tempPos);
            Debug.Log("Fall");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Boulder_Falls();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag ("Player"))
        {
            Destroy(gameObject);
            FindObjectOfType<PlayerController>().Hurt_Player(values.damage);
        }
    }
}
