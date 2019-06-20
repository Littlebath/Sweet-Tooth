using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HeartDisplay : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    public Player_ScriptableObject pso;

    // Start is called before the first frame update
    void Start()
    {
        Init_Hearts();
        Update_Hearts();


    }

    // Update is called once per frame
    void Update()
    {
        Update_Hearts();

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Pulse activate");
            StartCoroutine(Pulse());
        }
    }

    public void Init_Hearts ()
    {
        float heartContainers = pso.maxHealth / 2;

        for (int i = 0; i < heartContainers; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    public IEnumerator Pulse ()
    {
        Debug.Log("Pulse");
        float t = 0;
        Vector3 orignalSize = transform.localScale;
        Vector3 newSize = new Vector3(1.3f, 1.3f, 1f);

        for (int i = 0; i < 1 / 0.3; i++)
        {
            gameObject.GetComponent<RectTransform>().localScale = Vector3.Lerp(transform.localScale, newSize, t);
            yield return new WaitForSeconds(0.05f);
            t += 0.7f;
        }

        yield return new WaitForEndOfFrame();

        t = 0;

        for (int i = 0; i < 1 / 0.3f; i++)
        {
            gameObject.GetComponent<RectTransform>().localScale = Vector3.Lerp(transform.localScale, orignalSize, t);
            yield return new WaitForSeconds(0.05f);
            t += 0.7f;
        }
    }

    public void Update_Hearts ()
    {
        float tempHealth = pso.health / 2;
        float heartContainers = pso.maxHealth / 2;

        //Debug.Log(tempHealth);

        for (int i = 0; i < heartContainers; i++)
        {
            if (i <= tempHealth - 1)
            {
                //Full Heart
                hearts[i].sprite = fullHeart;
            }

            else if (i >= tempHealth)
            {
                //Empty heart
                hearts[i].sprite = emptyHeart;
            }

            else
            {
                //Half heart
                hearts[i].sprite = halfHeart;
            }
        }
    }
}
