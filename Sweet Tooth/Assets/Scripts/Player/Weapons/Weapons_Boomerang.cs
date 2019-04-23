using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons_Boomerang : MonoBehaviour
{
    //Scriptable Object
    [SerializeField] private Player_ScriptableObject psc;

    //Vectors
    private Vector2 startPos;
    private Vector2 playerPos;

    //Floats
    private float currentSpeed;
    private float currentDistance;

    //Bools

    [HideInInspector] public bool isMovingAway = true;
    [HideInInspector] public bool hasStopped;
    [HideInInspector] public bool isMovingToPlayer;
    [HideInInspector] public bool isPowered;

    //Integers
    private int direction = 0;

    //References
    [HideInInspector] public Rigidbody2D rb2d;

    //Scripts
    private PlayerInput pi;
    private PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        pi = FindObjectOfType<PlayerInput>();
        pc = FindObjectOfType<PlayerController>();

        rb2d = gameObject.GetComponent<Rigidbody2D>();

        startPos = gameObject.transform.position;

        BoomerangDifference();
        Direction_Of_Boomerang();
    }

    // Update is called once per frame
    void Update()
    {
        Boomerang_Movement();
        //Call_Back_Boomerang();

        transform.Rotate(0f, 0f, psc.boomerangTurnSpeed);
    }

    void Direction_Of_Boomerang ()
    {
        if (pc.isMoving)
        {
            
            if (pc.lastMove.y > 0)
            {
                //Debug.Log("Face up");
                rb2d.velocity = Vector2.up * currentSpeed;
                direction = 1;
            }

            else if (pc.lastMove.y < 0)
            {
                //Debug.Log("Face down");
                rb2d.velocity = Vector2.down * currentSpeed;
                direction = 2;
            }

            else if (pc.lastMove.x > 0)
            {
                //Debug.Log("Face right");
                rb2d.velocity = Vector2.right * currentSpeed;
                direction = 3;
            }

            else if (pc.lastMove.x < 0)
            {
                //Debug.Log("Face left");
                rb2d.velocity = Vector2.left * currentSpeed;
                direction = 4;
            }


            //Do moving animations
            /*if (p.verticalInput != 0)
            {
                if (pi.verticalInput > 0)
                {
                    //Debug.Log("Moving up");
                    rb2d.velocity = Vector2.up * currentSpeed;
                    direction = 1;
                }

                else if (pi.verticalInput < 0)
                {
                    //Debug.Log("Moving down");
                    rb2d.velocity = Vector2.down * currentSpeed;
                    direction = 2;

                }
            }

            else
            {
                if (pi.horizontalInput > 0)
                {
                    //Debug.Log("Moving right");
                    rb2d.velocity = Vector2.right * currentSpeed;
                    direction = 3;
                }

                else if (pi.horizontalInput < 0)
                {
                    //Debug.Log("Moving left");
                    rb2d.velocity = Vector2.left * currentSpeed;
                    direction = 4;
                }
            }*/
        }

        else
        {
            if (pc.lastMove.x > 0)
            {
                //Debug.Log("Face right");
                rb2d.velocity = Vector2.right * currentSpeed;
                direction = 3;
            }

            else if (pc.lastMove.x < 0)
            {
                //Debug.Log("Face left");
                rb2d.velocity = Vector2.left * currentSpeed;
                direction = 4;
            }

            else if (pc.lastMove.y > 0)
            {
                //Debug.Log("Face up");
                rb2d.velocity = Vector2.up * currentSpeed;
                direction = 1;
            }

            else if (pc.lastMove.y < 0)
            {
                //Debug.Log("Face down");
                rb2d.velocity = Vector2.down * currentSpeed;
                direction = 2;
            }
        }

        switch (direction)
        {
            case 4:
                //Goes Left
                break;

            case 3:
                //Goes Right
                break;

            case 2:
                //Goes down
                break;

            case 1:
                //Goes up
                break;
        }
    }

    void Boomerang_Movement ()
    {
        if (isMovingAway)
        {
            if (rb2d.velocity != Vector2.zero)
            {
                if (Vector2.Distance(transform.position, startPos) >= currentDistance)
                {
                    rb2d.velocity = Vector2.zero;
                    isMovingAway = false;
                    hasStopped = true;
                }
            }
        }
    }

    public void Call_Back_Boomerang ()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        if (!isMovingAway)
        {
            if (isMovingToPlayer)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerPos, currentSpeed * Time.deltaTime);
            }
        }
    }

    void BoomerangDifference ()
    {
        if (isPowered)
        {
            StartCoroutine(Power_Boomerang());
        }

        else
        {
            StartCoroutine(Normal_Boomerang());
        }
    }

    public IEnumerator Power_Boomerang ()
    {
        currentSpeed = psc.boomerangPowerSpeed;
        currentDistance = psc.boomerangPowerDistance;
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        //Debug.Log("Is Powered");
        yield return null;
    }

    IEnumerator Normal_Boomerang ()
    {
        currentSpeed = psc.boomerangSpeed;
        currentDistance = psc.boomerangDistance;
        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isMovingToPlayer = false;

        if (collision.gameObject.CompareTag ("Enemy"))
        {
            //collision.gameObject.GetComponent<Enemy>().Take_Damage(psc.boomerangDamage);
        }

        Destroy(gameObject);
    }
}
