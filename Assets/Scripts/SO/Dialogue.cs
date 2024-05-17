using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Dialogue")]
public class Dialogue : ScriptableObject
{
    [TextArea]
    public string[] lines;
}
