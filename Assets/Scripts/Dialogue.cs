using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/TradeableItem")]
public class Dialogue : ScriptableObject
{
    [TextArea]
    public string[] Dialogues;
}
