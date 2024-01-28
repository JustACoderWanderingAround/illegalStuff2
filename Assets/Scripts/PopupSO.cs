using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PopupSO")]
public class PopupSO : ScriptableObject
{
    [TextArea(5, 5)]
    public string[] popupLines;
}
