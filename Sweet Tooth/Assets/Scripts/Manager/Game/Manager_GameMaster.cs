using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager_GameMaster : MonoBehaviour
{
    public static Manager_GameMaster instance;

    private GameObject player;

    private Player_ScriptableObject psc;
	// Use this for initialization
	void Start ()
    {
        //DontDestroy();
        player = GameObject.FindGameObjectWithTag("Player");
        psc = Resources.Load<Player_ScriptableObject>("Scriptable Objects/Player/Player Values");
    }
	
	// Update is called once per frame
	void Update ()
    {
       
    }

    void DontDestroy ()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void PlayerRespawn()
    {
        StartCoroutine(TrueRespawn());
    }

    public IEnumerator TrueRespawn()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        player.GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponentInChildren<Animator>().SetTrigger("died");
        //player.SetActive(false);
        FindObjectOfType<Fading>().FadeOut();
        //Debug.Log("Died");
        yield return new WaitForSeconds(1.5f);
        player.GetComponentInChildren<Animator>().SetTrigger("playerAlive");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<Fading>().FadeIn();
        player.GetComponent<BoxCollider2D>().enabled = true;
        FindObjectOfType<UI_HeartDisplay>().Init_Hearts();
        FindObjectOfType<UI_HeartDisplay>().Update_Hearts();

        if (psc.health <= 0)
        {
            psc.ResetValues();
        }
    }
}
