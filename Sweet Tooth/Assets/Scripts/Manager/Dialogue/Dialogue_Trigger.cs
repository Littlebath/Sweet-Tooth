using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Trigger : MonoBehaviour
{
    //public Dialogue dialogue;
    public List<DialogueScriptableObject> dialogue;

    public void TriggerDialogue ()
    {
        FindObjectOfType<Manager_Dialogue>().StartDialogue(dialogue);
    }

}
