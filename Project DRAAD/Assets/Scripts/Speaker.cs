using UnityEngine;

[CreateAssetMenu(fileName = "Speaker", menuName = "ScriptableObjects/Speaker")]
public class Speaker : ScriptableObject
{
    public string speakerName;
    public Sprite speakerPortrait;
}
