using UnityEngine;

public class TestPointable : MonoBehaviour, IPointable
{
    public Sprite thisSnapshotSprite;
    public string thisSnapshotText;

    [HideInInspector] public Sprite snapshotSprite => thisSnapshotSprite;
    [HideInInspector] public string snapshotText => thisSnapshotText;

    public bool Look(PlayerConversant playerConversant)
    {
        return true;
    }
}
