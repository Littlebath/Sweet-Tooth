using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC Face Sprites", menuName = "NPC/Face Sprites", order = 1)]

public class NPCFaceScriptableObject : ScriptableObject
{
    [Space]
    [Header("NPC sprite properties")]
    [Space]
    [Header("NPC changes direction when the player talks to them")]
    [Space(25)]


    [Header("Direction the sprites are facing")]
    public Sprite facingUp;
    public Sprite facingLeft;
    public Sprite facingDown;
    public Sprite facingRight;
}
