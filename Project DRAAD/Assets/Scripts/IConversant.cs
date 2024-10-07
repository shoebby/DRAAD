using UnityEngine;

public interface IConversant
{
    string conversantName { get; }
    Sprite conversantSprite { get; }
    Dialogue conversantDialogue { get; }

    bool Converse(PlayerConversant playerConversant);
}
