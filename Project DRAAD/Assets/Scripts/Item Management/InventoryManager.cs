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
    [SerializeField] private GameObject holdingHand;
    [SerializeField] private Animator holdingHand_anim;
    [SerializeField] private HeldItemData heldItemData;
    public Item heldItem;

    private bool inventoryIsOpen;
    private bool isHolding;

    private object[] itemsAsObjects;
    public SerializableDictionary<string, Item> keyItems = new SerializableDictionary<string, Item>();

    protected override void Awake()
    {
        base.Awake();

        inventoryBG.SetActive(false);
        holdingHand_anim.Play("holdingHand_closed");
        inventoryIsOpen = false;
        isHolding = false;

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
            {
                if (isHolding)
                    WithdrawItem();

                OpenInventory();
            }
            else if (inventoryIsOpen)
                CloseInventory();

        if (Input.GetMouseButtonDown(1) && isHolding)
            WithdrawItem();
    }

    public void OpenInventory()
    {
        foreach (KeyValuePair<string, Item> pair in keyItems)
        {
            if (CheckHasItem(pair.Key) == true)
            {
                GameObject itemEntry = Instantiate(itemEntryPrefab, inventoryContainer.transform);

                inventoryLayouter.children.Add(itemEntry.transform);
                itemEntry.GetComponent<ItemEntryData>().entry_item = pair.Value;
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

    public void HoldItem(Item item)
    {
        heldItem = item;
        heldItemData.heldItem_item = heldItem;
        isHolding = true;

        holdingHand_anim.Play("holdingHand_open");

        Debug.Log("Currently holding: " + heldItem.itemName);
        CloseInventory();
    }

    public void WithdrawItem()
    {
        if (heldItem != null)
        {
            holdingHand_anim.Play("holdingHand_close");
            heldItem = null;
            isHolding = false;
        }
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

    public void AddItem(string itemID)
    {
        keyItems.TryGetValue(itemID, out Item item);
        item.hasItem = true;
    }

    public void RemoveItem(string itemID)
    {
        keyItems.TryGetValue(itemID, out Item item);
        item.hasItem = false;
    }
}
