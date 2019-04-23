using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player_Start : MonoBehaviour
{
    private PlayerController pc;
    private CinemachineVirtualCamera cc;

    [Header("Spawn ID")]
    public string pointName;

    private Checkpoints cp;


    public enum PlayerDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public PlayerDirection playerDirection;

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(Spawn_Player());
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Inventory>().inventorySystem = FindObjectOfType<Inventory_System>().transform.GetChild(0).gameObject;
        cp = gameObject.GetComponent<Checkpoints>();

        if (cp != null)
        {
            pointName = gameObject.name;
            //FindObjectOfType<Fading>().FadeIn();
            FindObjectOfType<CameraController>().Update_Cameras();
        }

        //cc.Set_Cameras();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public IEnumerator Spawn_Player ()
    {
        yield return new WaitForSeconds(0.2f);
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //Debug.Log(pointName);

        //Debug.Log("Change player location");

        if (pc.startPoint == pointName)
        {
            pc.transform.position = transform.position;

            cc = FindObjectOfType<CinemachineVirtualCamera>();
            cc.transform.position = new Vector3(transform.position.x, transform.position.y, cc.transform.position.z);
            Set_Player_Direction();
        }

    }


    private void Set_Player_Direction()
    {
        if (playerDirection == PlayerDirection.Down)
        {
            pc.lastMove.x = 0f;
            pc.lastMove.y = -1f;
        }

        else if (playerDirection == PlayerDirection.Left)
        {
            pc.lastMove.x = -1f;
            pc.lastMove.y = 0f;
        }


        else if (playerDirection == PlayerDirection.Up)
        {
            pc.lastMove.x = 0f;
            pc.lastMove.y = 1f;
        }


        else if (playerDirection == PlayerDirection.Right)
        {
            pc.lastMove.x = 1f;
            pc.lastMove.y = 0f;
        }
    }
}
