using TMPro;
using UnityEngine;

public class ItemEntryData : MonoBehaviour
{
    public Item entry_item;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TextMeshPro textMesh;

    private void Awake()
    {
        spriteRenderer.sprite = entry_item.itemSprite;
        textMesh.text = entry_item.itemName;
    }
}
