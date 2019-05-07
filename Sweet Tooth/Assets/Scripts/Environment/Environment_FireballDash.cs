using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum whoIsOnFire
{
    NoOne,
    Player,
    Enemy
}

public class Environment_FireballDash : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private float burnTimer;
    [SerializeField] private float burningDamage;

    public whoIsOnFire burner;

    private Animator anim;
    [SerializeField] private AnimationClip snuffFlames;
    [SerializeField] private Player_ScriptableObject pso;

    private bool isDying;
    private bool onFire;
    private bool canDash;


    private float countDown;

    private Collider2D enemy;

    private PlayerController pc;
    private PlayerInput pi;

    private Rigidbody2D playerRb2d;
    private Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        burner = whoIsOnFire.NoOne;
        countDown = pso.burnTimeOnEnemy;
        anim = gameObject.GetComponent<Animator>();
        StartCoroutine(Snuff_Flames());

        playerRb2d = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerAnim = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy != null)
        {
            if (countDown <= 0)
            {
                if (enemy.GetComponent<Enemy>() != null)
                {
                    enemy.GetComponent<Enemy>().Take_Damage(pso.burnDamageOverTime);
                }

                if (enemy.GetComponent<Boss_OreoChocolateBoss>() != null)
                {
                    enemy.GetComponent<Boss_OreoChocolateBoss>().Take_Damage(pso.burnDamageOverTime);
                }

                countDown = pso.burnTimeOnEnemy;
            }

            else
            {
                countDown -= Time.deltaTime;
            }
        }

        if (pc == null)
        {
           pc = FindObjectOfType<PlayerController>();
        }

        if (pi == null)
        {
            pi = FindObjectOfType<PlayerInput>();
        }

        if (canDash)
        {
            Debug.Log("Can Dash");

            if (pi.dashButton)
            {
                Debug.Log("How many");
                pc.states = playerStates.Charging;
                StopCoroutine(Shoot_Player());
                StopCoroutine(Snuff_Flames());
                StartCoroutine(AutoDash());
            }
        }

        if (pc.states == playerStates.Charging)
        {
            pc.gameObject.transform.position = gameObject.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDying)
        {
            if (burner == whoIsOnFire.NoOne)
            {
                if (collision.gameObject.CompareTag("Enemy") || (collision.gameObject.CompareTag("Boss")))
                {
                    if (collision.gameObject.CompareTag("Boss"))
                    {
                        gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
                    }

                    Debug.Log("Hit enemy");
                    burner = whoIsOnFire.Enemy;
                    StopCoroutine(Snuff_Flames());
                    StartCoroutine(Burn_Enemy(collision));
                }

                if (collision.gameObject.CompareTag("Player"))
                {
                    Debug.Log("Hit Player");
                    burner = whoIsOnFire.Player;

                    if (collision.gameObject.GetComponent<PlayerController>().isDashing)
                    {
                        StartCoroutine(Shoot_Player());
                    }

                    else
                    {
                        StopCoroutine(Snuff_Flames());
                        gameObject.transform.position = pc.gameObject.transform.position;
                        gameObject.transform.parent = pc.gameObject.transform;

                        if (pso.energyCounter < pso.maxEnergy)
                        {
                            pso.energyCounter += pso.fireballRecovery;
                        }

                        isDying = true;
                        anim.SetTrigger("dies");
                        Destroy(gameObject, snuffFlames.averageDuration);
                    }

                }
            }
        }
    }

    IEnumerator Burn_Enemy (Collider2D other)
    {
        gameObject.transform.position = other.gameObject.transform.position;

        if (other.GetComponent<Enemy>() != null)
        {
            other.GetComponent<Enemy>().Take_Damage(pso.fireballDamage);
        }

        if (other.GetComponent<Boss_OreoChocolateBoss>() != null)
        {
            other.GetComponent<Boss_OreoChocolateBoss>().Take_Damage(pso.burnDamageOverTime);
        }

        gameObject.transform.parent = other.gameObject.transform;
        enemy = other;
        yield return new WaitForSeconds(1f);

        if (other.GetComponent<Enemy>() != null)
        {
            other.gameObject.GetComponent<Enemy>().anim.SetBool("isHurt", false);
        }

        isDying = true;
        anim.SetTrigger("dies");
        Destroy(gameObject, snuffFlames.averageDuration);
    }

    IEnumerator Snuff_Flames ()
    {
        yield return new WaitForSeconds(timer);
        isDying = true;
        anim.SetTrigger("dies");
        Destroy(gameObject, snuffFlames.averageDuration);
    }


    IEnumerator Shoot_Player()
    {
        pc.gameObject.transform.position = gameObject.transform.position;

        pso.energyCounter++;
        pi.verticalInput = 0;
        pi.horizontalInput = 0;
        playerRb2d.velocity = Vector2.zero;
        playerAnim.SetBool("isMoving", false);
        pc.enabled = false;
        canDash = true;
        yield return new WaitForSeconds(3f);
        canDash = false;
        pc.enabled = true;
        pi.enabled = true;
        yield return null;
    }

    IEnumerator AutoDash()
    {
        pc.gameObject.transform.position = gameObject.transform.position;
        Debug.Log("Dew it");
        canDash = false;
        Vector2 input = new Vector2(pi.horizontalInput, pi.verticalInput);
        playerAnim.SetFloat("LastMoveX", pi.horizontalInput);
        playerAnim.SetFloat("LastMoveY", pi.verticalInput);
        playerRb2d.velocity = input * pso.dashSpeed;
        gameObject.GetComponent<Rigidbody2D>().velocity = input * pso.dashSpeed;
        yield return new WaitForSeconds(pso.dashTime * 4);
        isDying = true;
        anim.SetTrigger("dies");
        pc.states = playerStates.None;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        pc.enabled = true;
        pi.enabled = true;
        Destroy(gameObject, snuffFlames.averageDuration);
        yield return null;
    }

}
