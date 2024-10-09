using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Speaker", menuName = "ScriptableObjects/Speaker")]
public class Speaker : ScriptableObject
{
    public string speakerName;
    public Color speakerTextColor;
    public TMP_FontAsset speakerFont;
}
