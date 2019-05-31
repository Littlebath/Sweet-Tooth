using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Save Objects", menuName = "Save state")]
public class SaveStateObj : ScriptableObject
{
    public int saveState;

    public void ForceSerialization()
    {

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    public void ResetValues ()
    {
        saveState = 0;
    }
}
