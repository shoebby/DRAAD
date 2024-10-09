using UnityEngine;

public interface IPointable
{
    Sprite snapshotSprite { get; }
    string snapshotText { get; }

    bool Look(PlayerConversant playerConversant);
}
