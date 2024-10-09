using UnityEngine;

public interface IConversant
{
    string conversantName { get; }
    Color conversantTextColor { get; }
    Dialogue conversantDialogue { get; }

    bool Converse(PlayerConversant playerConversant);
}
