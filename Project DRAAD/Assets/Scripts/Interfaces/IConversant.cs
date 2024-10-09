using TMPro;
using UnityEngine;

public interface IConversant
{
    string conversantName { get; }
    Color conversantTextColor { get; }
    TMP_FontAsset conversantFont {  get; }
    Dialogue conversantDialogue { get; }

    bool Converse(PlayerConversant playerConversant);
}
