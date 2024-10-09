using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public bool hasItem;
}
