using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EnergyDisplay : MonoBehaviour
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
        Update_Energy();
    }

    // Update is called once per frame
    void Update()
    {
        Update_Energy();
    }

    public void Init_Hearts()
    {
        float heartContainers = pso.maxEnergy / 2;

        for (int i = 0; i < heartContainers; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    public void Update_Energy()
    {
        float tempHealth = pso.energyCounter / 2;
        float heartContainers = pso.maxEnergy / 2;

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
