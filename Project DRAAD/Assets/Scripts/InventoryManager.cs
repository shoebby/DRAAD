using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [Header("UI Components")]
    [SerializeField] private GameObject inventoryBG;
    [SerializeField] private GameObject inventoryContainer;
    [SerializeField] private InventoryLayouter inventoryLayouter;
    [SerializeField] private GameObject itemEntryPrefab;
    [SerializeField] private GameObject[] itemEntries;

    private bool inventoryIsOpen;

    private object[] itemsAsObjects;
    public SerializableDictionary<string, Item> keyItems = new SerializableDictionary<string, Item>();

    protected override void Awake()
    {
        base.Awake();

        inventoryBG.SetActive(false);
        inventoryIsOpen = false;

        itemsAsObjects = Resources.LoadAll("Items", typeof(Item));

        foreach (Item item in itemsAsObjects)
        {
            keyItems.Add(item.name, item);
        }
    }

    void Start()
    {
        foreach (KeyValuePair<string, Item> pair in keyItems)
        {
            Debug.Log(pair.Key + " - " + pair.Value);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            if (!inventoryIsOpen)
                OpenInventory();
            else if (inventoryIsOpen)
                CloseInventory();
    }

    public void OpenInventory()
    {
        foreach (KeyValuePair<string, Item> pair in keyItems)
        {
            if (CheckHasItem(pair.Key) == true)
            {
                GameObject itemEntry = Instantiate(itemEntryPrefab, inventoryContainer.transform);

                inventoryLayouter.children.Add(itemEntry.transform);
                itemEntry.GetComponentInChildren<TextMeshPro>().text = pair.Value.itemName;
                itemEntry.GetComponentInChildren<SpriteRenderer>().sprite = pair.Value.itemSprite;
            }
        }

        inventoryBG.SetActive(true);
        inventoryIsOpen = true;
    }

    public void CloseInventory()
    {
        foreach(Transform child in inventoryContainer.transform)
        {
            Destroy(child.gameObject);
        }

        inventoryLayouter.children.Clear();
        inventoryBG.SetActive(false);
        inventoryIsOpen = false;
    }

    public bool CheckHasItem(string itemID)
    {
        keyItems.TryGetValue(itemID, out Item item);
        return item.hasItem;
    }

    public Item GetKeyItem(string itemID)
    {
        keyItems.TryGetValue(itemID, out Item item);
        return item;
    }
}
