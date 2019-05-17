using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Quests", menuName ="Quest 1")]
public class Quest1ScriptableObject : ScriptableObject
{
    [Header("Quest 1 values")]
    public bool[] pathA;
    public bool[] pathB;
    public bool[] pathC;
    public bool questCompleted;

    [Header("Quest 2 values")]
    public bool [] objectives2;
    public bool quest2Completed;

    [Header("Quest 3 values")]
    public bool[] objectives3A;
    public bool[] objectives3B;
    public bool quest3Completed;


    public void ForceSerialization()

    {
        #if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }

    public void ResetQuest ()
    {
        for (int i = 0; i < pathA.Length; i++)
        {
            pathA[i] = false;
        }

        for (int i = 0; i < pathB.Length; i++)
        {
            pathB[i] = false;
        }

        for (int i = 0; i < pathC.Length; i++)
        {
            pathC[i] = false;
        }

        questCompleted = false;
    }
}
