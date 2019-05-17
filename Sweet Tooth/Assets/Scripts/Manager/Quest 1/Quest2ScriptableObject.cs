using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest 2")]
public class Quest2ScriptableObject : ScriptableObject
{
    public bool[] objectives;

    public bool questCompleted;
}
