using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/DialogueSO", order = 1)]

public class DialogueScriptableObject : ScriptableObject
{
    [Header("Name of speaker")]
    public string name;

    [Header("Picture of speaker")]
    public Sprite portrait;

    [Header("List of all possible dialogues")]
    [TextArea(3, 10)]
    public string[] sentences;

    public bool followDialogue;
}
