using UnityEngine;

public class HeldItemData : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public Item heldItem_item;

    private void Update()
    {
        if (heldItem_item == null) return;
        
        spriteRenderer.sprite = heldItem_item.itemSprite;
    }
}
