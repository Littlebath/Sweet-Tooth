using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    //Designer Values
    public float moveSpeed = 5.0f;

    //Bools
    private static bool isCameraExisting;
    [HideInInspector] public bool isFollowingPlayer = true;

    //Gameobjects
    private GameObject player;

    //Vectors
    private Vector3 targetPos;

    //Scripts
    //[SerializeField]private GameObject [] cameras;

    [SerializeField] private Animator anim;

    [SerializeField] private GameObject camera;


	// Use this for initialization
	void Start ()
    {
        //DestroyDuplicates();

        anim = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isFollowingPlayer)
        {
            //FollowPlayer();            
        }

        Update_Cameras();
    }

    void FollowPlayer ()
    {
        targetPos = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

    void DestroyDuplicates ()
    {
        if (!isCameraExisting)
        {
            isCameraExisting = true;
            DontDestroyOnLoad(transform.gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void Screen_Kick ()
    {
        camera.GetComponent<Animator>().SetTrigger("kick");
        //anim.SetTrigger("kick");
    }

    public void Update_Cameras ()
    {
        if (camera == null)
        {
            camera = FindObjectOfType<CinemachineVirtualCamera>().gameObject;
        }
    }

    public void Update_Dungeon_Camera ()
    {
        camera = FindObjectOfType<CinemachineVirtualCamera>().gameObject;
    }
}
