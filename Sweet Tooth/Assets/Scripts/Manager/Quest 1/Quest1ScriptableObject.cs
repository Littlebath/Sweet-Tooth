using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Quests", menuName ="Quest 1")]
public class Quest1ScriptableObject : ScriptableObject
{
    [Header("Alternating Pathways")]
    public bool[] pathA;
    public bool[] pathB;
    public bool[] pathC;

    public void ForceSerialization()
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
