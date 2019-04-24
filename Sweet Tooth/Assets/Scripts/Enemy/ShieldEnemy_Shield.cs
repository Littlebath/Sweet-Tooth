using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy_Shield : MonoBehaviour
{
    [SerializeField] private float shieldHealth;
    [SerializeField] private Transform shieldTransform;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = shieldTransform.position;
        StartCoroutine(ChangeAnim(gameObject.transform.parent.GetChild(0).GetComponent<Enemy_Log>().SuperPos));
    }

    public void Damage_Shield (float damage)
    {
        shieldHealth -= damage;

        if (shieldHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Set_Anim_Float(Vector2 setVector)
    {
        if (gameObject.transform.parent.GetChild(0).GetComponent<Enemy_Log>().timeBtwChangeDirectionCounter <= 0)
        {
            anim.SetFloat("moveX", setVector.x);
            anim.SetFloat("moveY", setVector.y);
        }

        else
        {
            gameObject.transform.parent.GetChild(0).GetComponent<Enemy_Log>().timeBtwChangeDirectionCounter -= Time.deltaTime;
        }
    }

    private IEnumerator ChangeAnim(Vector3 dir)
    {
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if (dir.x > 0)
            {
                yield return new WaitForSeconds(gameObject.transform.parent.GetChild(0).GetComponent<Enemy_Log>().timeBtwChangeDirection);
                Set_Anim_Float(Vector2.right);
                //timeBtwChangeDirectionCounter = timeBtwChangeDirection;
            }

            else if (dir.x < 0)
            {
                yield return new WaitForSeconds(gameObject.transform.parent.GetChild(0).GetComponent<Enemy_Log>().timeBtwChangeDirection);
                Set_Anim_Float(Vector2.left);
                //timeBtwChangeDirectionCounter = timeBtwChangeDirection;
            }
        }

        else if (Mathf.Abs(dir.x) < Mathf.Abs(dir.y))
        {
            if (dir.y > 0)
            {
                yield return new WaitForSeconds(gameObject.transform.parent.GetChild(0).GetComponent<Enemy_Log>().timeBtwChangeDirection);
                Set_Anim_Float(Vector2.up);
                //timeBtwChangeDirectionCounter = timeBtwChangeDirection;
            }

            else if (dir.y < 0)
            {
                yield return new WaitForSeconds(gameObject.transform.parent.GetChild(0).GetComponent<Enemy_Log>().timeBtwChangeDirection);
                Set_Anim_Float(Vector2.down);
                //timeBtwChangeDirectionCounter = timeBtwChangeDirection;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!collision.gameObject.GetComponent<Rigidbody2D>().isKinematic)
            {
                collision.gameObject.GetComponent<PlayerController>().Hurt_Player(gameObject.transform.parent.GetChild(0).GetComponent<Enemy>().baseAttack);
            }
        }
    }
}
