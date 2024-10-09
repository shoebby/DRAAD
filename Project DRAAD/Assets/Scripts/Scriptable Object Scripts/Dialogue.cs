using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue")]
public class Dialogue : ScriptableObject
{
    [TextArea(0,25)]
    public string[] dialogueText;
    public int[] speakerIDs;
    public Speaker[] speakers;
}
