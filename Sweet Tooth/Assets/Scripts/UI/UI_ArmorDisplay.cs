using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ArmorDisplay : MonoBehaviour
{

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    public Player_ScriptableObject pso;

    private Color originalColor;

    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
        //originalColor = hearts[0].color;
        Init_Hearts();
        Update_Hearts();
    }

    // Update is called once per frame
    void Update()
    {
        Update_Hearts();
    }

    public void Init_Hearts()
    {
        float heartContainers = pso.maxArmor / 2;

        for (int i = 0; i < heartContainers; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    public void Update_Hearts()
    {
        float tempHealth = pso.armor / 2;
        float heartContainers = pso.maxArmor / 2;

        for (int i = 0; i < heartContainers; i++)
        {
            if (i <= tempHealth - 1)
            {
                //Full Heart
                hearts[i].sprite = fullHeart;
                hearts[i].color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
            }

            else if (i >= tempHealth)
            {
                //Empty heart
                hearts[i].sprite = emptyHeart;
                hearts[i].color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
            }

            else
            {
                //Half heart
                hearts[i].sprite = halfHeart;
                hearts[i].color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
            }
        }
    }
}

