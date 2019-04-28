using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Manager_Timeline : MonoBehaviour
{
    private bool fix;

    public Animator player;
    public RuntimeAnimatorController playerAnim;
    public PlayableDirector director;


    // Start is called before the first frame update
    void Start ()
    {
        playerAnim = player.runtimeAnimatorController;
        player.runtimeAnimatorController = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (director.state != PlayState.Playing && !fix)
        {
            fix = true;
            player.runtimeAnimatorController = playerAnim;
        }
    }
}
