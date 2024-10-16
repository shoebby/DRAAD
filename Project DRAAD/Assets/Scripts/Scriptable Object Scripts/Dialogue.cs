using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue")]
public class Dialogue : ScriptableObject
{
    public string dialogueID;
    [TextArea(0,25)]
    public string[] dialogueText;
    public int[] speakerIDs;
    public Speaker[] speakers;
}
