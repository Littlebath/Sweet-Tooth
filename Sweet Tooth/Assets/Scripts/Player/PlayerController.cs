using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum playerStates
{
    None,
    Charging,
}

public class PlayerController : MonoBehaviour
{
    //Scriptable Objects
    public Player_ScriptableObject designerValues;

    //Gameobjects
    [HideInInspector] public GameObject spawnPoint;

    //Bools
    [HideInInspector] public bool isMoving;
    [HideInInspector] public bool isDashing;
    [HideInInspector] public bool fireballFired;
    [HideInInspector] public bool isMelee;
    private bool isDashingToBoomerang;
    private bool thrownBoomerang;
    [HideInInspector] public bool isSpinning;

    private static bool isPlayerExisting;

    //Transform
    [HideInInspector] public Transform attackPos;

    //floats
    [HideInInspector] public float currentMoveSpeed;
    private float timeBtwAttack;
    private float energyTimeCounter;

    //strings
    public string startPoint;

    //Vector2
    [HideInInspector] public Vector2 lastMove;

    //References
    private Animator anim;
    [HideInInspector] public Rigidbody2D rb2d;

    //Scripts
    private PlayerInput pi;

    public playerStates states;

    // Use this for initialization
    void Start ()
    {
        DestroyDuplicates();

        anim = gameObject.GetComponentInChildren<Animator>();
        pi = FindObjectOfType<PlayerInput>();

        attackPos = gameObject.transform.GetChild(0).GetChild(2).transform;
        spawnPoint = gameObject.transform.GetChild(0).GetChild(1).transform.gameObject;

        designerValues.boomerang = Resources.Load("Prefabs/Player/Weapons/Boomerang") as GameObject;

        rb2d = gameObject.GetComponent<Rigidbody2D>();

        designerValues.ResetValues();

        energyTimeCounter = designerValues.energyRegenerateTime;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (designerValues.hasDash)
        {
            if (rb2d.velocity != Vector2.zero)
            {
                Dash();
            }
        }

        SetAnimations();

        if (designerValues.health > designerValues.maxHealth)
        {
            designerValues.health = designerValues.maxHealth;
        }

        Regenerate_Energy();
    }

    private void Regenerate_Energy()
    {
        if (energyTimeCounter <= 0)
        {
            designerValues.energyCounter += designerValues.energyRegenerateAmount;
            energyTimeCounter = designerValues.energyRegenerateTime;
            //Debug.Log("added amount");
        }

        else
        {
            energyTimeCounter -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        //GridMovement();
        if (gameObject.GetComponent<Rigidbody2D>().isKinematic == false)
        {
            EightDirectionalMovement();
            Combat();
        }
    }


    private void Combat()
    {
        if (FindObjectOfType<Manager_Dialogue>().isTalking == false)
        {
            Melee();
        }
        //BoomerangCombat();
        FireBallCombat();
    }

    private void FireBallCombat()
    {
        if (designerValues.hasFireball)
        {
            if (pi.attackButton)
            {
                if (!fireballFired)
                {
                    if (designerValues.energyCounter >= designerValues.fireballEnergyUse)
                    {
                        designerValues.energyCounter -= designerValues.fireballEnergyUse;
                        fireballFired = true;
                        Instantiate(designerValues.fireball, spawnPoint.transform.position, transform.rotation);
                        StartCoroutine(Reset_FireRate());
                    }
                }
            }
        }
    }

    private IEnumerator Reset_FireRate ()
    {
        //Debug.Log("Die");
        yield return new WaitForSeconds (designerValues.fireballFireRate);
        fireballFired = false;
    }

    private void BoomerangCombat ()
    {
        if (pi.attackButton)
        {
            if (designerValues.boomerang != null)
            {
                //Throw Boomerang
                StartCoroutine(Throw_Boomerang());
            }

            /*else
            {
                // Call it back
                Weapons_Boomerang boomerang = FindObjectOfType<Weapons_Boomerang>();
                boomerang.rb2d.velocity = Vector2.zero;
                boomerang.isMovingToPlayer = true;
                boomerang.hasStopped = true;
                boomerang.isMovingAway = false;
            }*/
        }

        //SpinningAttack();

        /*if (isDashing)
        {
            if (pi.attackButton)
            {
                if (designerValues.boomerang == null)
                {
                    Weapons_Boomerang boomerang = FindObjectOfType<Weapons_Boomerang>();
                    boomerang.isPowered = true;
                    StartCoroutine(boomerang.Power_Boomerang());
                    //boomerang.BoomerangDifference();
                }
            }
        }*/

        //DashingToBoomerang();

        //Knock_Up_System();
    }

    private void Knock_Up_System()
    {
        if (Input.GetButton("Dash"))
        {
            if (pi.meleeButton)
            {
                isDashing = true;
                Debug.Log("Knock Up Attack");
                StartCoroutine(Dashing());
            }
        }
    }

    private void DashingToBoomerang()
    {
        if (designerValues.boomerang == null)
        {
            if (pi.dashToBoomerangButton)
            {
                //Dash to boomerang
                Debug.Log("Go To Boomerang");
                isDashingToBoomerang = true;
            }
        }

        else
        {
            isDashingToBoomerang = false;
        }

        if (isDashingToBoomerang)
        {
            transform.position = Vector2.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Boomerang").transform.position, designerValues.dashSpeed * Time.deltaTime);
            gameObject.layer = 11;
        }

        else
        {
            gameObject.layer = 8;
        }
    }

    private void SpinningAttack ()
    {
        if (designerValues.boomerang != null)
        {
            if (pi.spinAttackButton)
            {
                isSpinning = true;
                currentMoveSpeed = 0;

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, designerValues.spinRange, designerValues.whatIsEnemy);

                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    Debug.Log(enemiesToDamage[i].gameObject.name);
                    //enemiesToDamage[i].gameObject.GetComponent<Enemy>().Take_Damage(designerValues.meleeDamage);
                    //FindObjectOfType<Player_Knockback>().Knock_Back_Enemies(enemiesToDamage[i]);
                }
            }

            else
            {
                currentMoveSpeed = designerValues.moveSpeed;
                isSpinning = false;
            }
        }

        else
        {
            isSpinning = false;
        }
    }

    void Melee ()
    {
        //Debug.Log("Can Melee");

        if (Input.GetButton("Dash") == false)
        {
            if (timeBtwAttack <= 0)
            {
                isMelee = false;

                if (!isMoving)
                {
                    if (!NPC.canMelee)
                    {
                        if (pi.meleeButton)
                        {
                            timeBtwAttack = designerValues.meleeTime.averageDuration;

                            //Explosive Nuts
                            Collider2D[] nutExplodes = Physics2D.OverlapCircleAll(attackPos.position, designerValues.meleeRange, designerValues.whatIsNuts);

                            for (int i = 0; i < nutExplodes.Length; i++)
                            {
                                StartCoroutine(nutExplodes[i].GetComponent<Environment_ExplosiveNut>().Explode());
                            }

                            //Cinnamon
                            Collider2D[] cinnamonBurns = Physics2D.OverlapCircleAll(attackPos.position, designerValues.meleeRange, designerValues.whatIsCinnamon);

                            for (int i = 0; i < cinnamonBurns.Length; i++)
                            {
                                StartCoroutine(cinnamonBurns[i].GetComponent<Environment_Cinnamon>().Set_Alight());
                            }
                        }
                    }
                }
            }

            else
            {
                timeBtwAttack -= Time.deltaTime;
                isMelee = true;
            }

        }

        if (isMelee)
        {

            //Enemies
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, designerValues.meleeRange, designerValues.whatIsEnemy);

            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                if (enemiesToDamage[i].GetComponent<Rigidbody2D>() != null && enemiesToDamage[i].GetComponent<Rigidbody2D>().isKinematic)
                {
                    //Debug.Log(enemiesToDamage[i].gameObject.name);
                    enemiesToDamage[i].gameObject.GetComponent<Enemy>().Take_Damage(designerValues.meleeDamage);
                    //enemiesToDamage[i].gameObject.GetComponent<Enemy_KnockUp>().Knock_Up();
                    FindObjectOfType<Player_Knockback>().Knock_Back(enemiesToDamage[i]);
                }
            }

            //Breakable Objects
            Collider2D [] breakablesToDestroy = Physics2D.OverlapCircleAll(attackPos.position, designerValues.meleeRange, designerValues.whatIsBreakables);

            for (int i = 0; i < breakablesToDestroy.Length; i++)
            {
                breakablesToDestroy[i].GetComponent<pot>().Smash();
            }

            //Gum
            Collider2D[] gumToMelt = Physics2D.OverlapCircleAll(attackPos.position, designerValues.meleeRange, designerValues.whatIsGum);

            for (int i = 0; i < gumToMelt.Length; i++)
            {
                gumToMelt[i].GetComponent<Environment_Gum>().Spawn_Sticky();
                Debug.Log("Got one");
            }

            //Rasgulla
            Collider2D[] rasgullaExpands = Physics2D.OverlapCircleAll(attackPos.position, designerValues.meleeRange, designerValues.whatIsRasgulla);

            for (int i = 0; i < rasgullaExpands.Length; i++)
            {
                StartCoroutine(rasgullaExpands[i].GetComponent<Environment_Metal>().Metal_Process());
            }
        }
    }

    private IEnumerator Throw_Boomerang()
    {
        if (!isSpinning)
        {
            anim.SetTrigger("BoomerangThrown");
            designerValues.boomerang.GetComponent<Weapons_Boomerang>().isPowered = false;
            yield return new WaitForSeconds(designerValues.throwTime.averageDuration / 2);

            if (designerValues.boomerang != null)
            {
                Instantiate(designerValues.boomerang, spawnPoint.transform.position, Quaternion.identity);
            }

            else
            {
                Debug.Log("No boomerang");
            }

            designerValues.boomerang = null;
            yield return null;
        }

        else
        {
            anim.SetTrigger("BoomerangThrown");
            designerValues.boomerang.GetComponent<Weapons_Boomerang>().isPowered = true;
            yield return new WaitForSeconds(designerValues.throwTime.averageDuration/2);

            if (designerValues.boomerang != null)
            {
                Instantiate(designerValues.boomerang, spawnPoint.transform.position, Quaternion.identity);
            }

            else
            {
                Debug.Log("No boomerang");
            }

            designerValues.boomerang = null;
            yield return null;
        }
        
    }
   
    void Dash()
    {
        if (isDashing)
        {
            StartCoroutine(Dashing());
        }

        if (pi.dashButton)
        {
            if (designerValues.energyCounter > 0)
            {
                isDashing = true;
                designerValues.energyCounter--;
            }
        }
    }

    IEnumerator Dashing ()
    {
        if (Mathf.Abs(pi.horizontalInput) > 0.5f && Mathf.Abs(pi.verticalInput) > 0.5f)
        {
            currentMoveSpeed = designerValues.dashSpeed * 0.75f;
        }

        else
        {
            currentMoveSpeed = designerValues.dashSpeed;
        }

        currentMoveSpeed = designerValues.dashSpeed;
        yield return new WaitForSeconds(designerValues.dashTime);
        currentMoveSpeed = designerValues.moveSpeed;
        isDashing = false;
    }

    void EightDirectionalMovement ()
    {
        MovementCheck();

        rb2d.velocity = new Vector2(pi.horizontalInput * currentMoveSpeed, pi.verticalInput * currentMoveSpeed);
        anim.SetFloat("MoveX", rb2d.velocity.x);
        anim.SetFloat("MoveY", rb2d.velocity.y);
    }

    void MovementCheck ()
    {
        if (rb2d.velocity == Vector2.zero)
        {
            isMoving = false;
        }

        else
        {
            isMoving = true;
        }

        
        if (pi.verticalInput > 0.5f || pi.verticalInput <= -0.5f)
        {
            lastMove = new Vector2(0f, pi.verticalInput);
            //pi.horizontalInput = 0f;
        }


        else if (pi.horizontalInput > 0.5f || pi.horizontalInput < -0.5f)
        {
            lastMove = new Vector2(pi.horizontalInput, 0f);
            //pi.verticalInput = 0f;
        }

        DiagonalMovement();
;    }

    void DiagonalMovement ()
    {
        if (!isDashing)
        {
            if (!isSpinning)
            {
                if (Mathf.Abs(pi.horizontalInput) > 0.5f && Mathf.Abs(pi.verticalInput) > 0.5f)
                {
                    currentMoveSpeed = designerValues.moveSpeed * 0.75f;
                }

                else
                {
                    currentMoveSpeed = designerValues.moveSpeed;
                }
            }
        }

    }

    void DestroyDuplicates ()
    {
        if (!isPlayerExisting)
        {
            isPlayerExisting = true;
            DontDestroyOnLoad(transform.gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

    }

    void SetAnimations ()
    {        
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isMelee", isMelee);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
        anim.SetBool("holdingDash", Input.GetButton("Dash"));
    }


    public void Hurt_Player(int damage)
    {
        anim.SetTrigger("isHurt");
        FindObjectOfType<UI_HeartDisplay>().Update_Hearts();
        FindObjectOfType<CameraController>().Screen_Kick();

        if (designerValues.armor <= 0)
        {
            designerValues.health -= damage;
            designerValues.armor = 0;
        }

        else
        {
            float percentage = (100 - designerValues.armorMultiplier) / 100;
            designerValues.armor -= Mathf.Round(damage * percentage);
        }

        if (designerValues.health <= 0)
        {
            FindObjectOfType<Manager_GameMaster>().PlayerRespawn();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Enemy")
        {
            if (collision.gameObject.GetComponent<Environment_Cinnamon>() != null)
            {
                if (states == playerStates.Charging)
                {
                    StartCoroutine(collision.gameObject.GetComponent<Environment_Cinnamon>().Set_Alight());
                }
            }

            if (states == playerStates.Charging)
            {
                states = playerStates.None;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, designerValues.meleeRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, designerValues.spinRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, designerValues.knockUpRange);
    }
}
