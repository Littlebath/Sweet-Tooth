using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [Header("Name of speaker")]
    public string name;

    [Header("List of all possible dialogues")]
    [TextArea(3, 10)]
    public string[] sentences;
}
