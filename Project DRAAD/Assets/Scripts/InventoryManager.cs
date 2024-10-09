using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public SerializableDictionary<string, Item> keyItemsDic = new SerializableDictionary<string, Item>();

    [System.Serializable]
    public class ItemEntry
    {
        public string name;
        public Item item;
    }

    public ItemEntry[] keyItems;

    private void Awake()
    {
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void GetKeyItem(string itemID)
    {
        
    }
}
