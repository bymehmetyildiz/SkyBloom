using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<ItemData> startingItems;

    public List<InventoryItem> equipment;
    public Dictionary<ItemData_Equipment, InventoryItem> equipmentDictionary;

    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> inventoryDictionary;

    public List<InventoryItem> stash;
    public Dictionary<ItemData, InventoryItem> stashDictionary;

    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform stashSlotParent;
    [SerializeField] private Transform equipmentSlotParent;
    [SerializeField] private Transform statSlotParent;

    private UI_ItemSlot[] inventorySlot;
    private UI_ItemSlot[] stashSlot;
    private UI_EquipmentSlot[] equipmentSlot;
    private UI_StatSlot[] statSlot;

    [Header("Items Cooldown")]    
    private float lastUseTimeOfFlusk;
    private float flaskCooldown;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance.gameObject);
    }

    private void Start()
    {
        inventory = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();

        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemData_Equipment, InventoryItem>();

        stash = new List<InventoryItem>();
        stashDictionary = new Dictionary<ItemData, InventoryItem>();

        inventorySlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        stashSlot = stashSlotParent.GetComponentsInChildren<UI_ItemSlot>();
        equipmentSlot = equipmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();
        statSlot = statSlotParent.GetComponentsInChildren<UI_StatSlot>();

        StartItems();

      
    }

    private void StartItems()
    {
        for (int i = 0; i < startingItems.Count; i++)
        {
            AddItem(startingItems[i]);
        }
    }

    // Equip Item
    public void EquipItem(ItemData _item)
    {
        ItemData_Equipment newEquipment = _item as ItemData_Equipment;
        InventoryItem newItem = new InventoryItem(newEquipment);

        ItemData_Equipment oldEquipment = null;

        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == newEquipment.equipmentType)
                oldEquipment = item.Key;
        }

        if (oldEquipment != null)
        {
            UnequipItem(oldEquipment);
            AddItem(oldEquipment);
        }


        equipment.Add(newItem);
        equipmentDictionary.Add(newEquipment, newItem);
        newEquipment.AddModifiers();

        RemoveItem(_item);
        UpdateSlotUI();
    }    

    // UnEquip Item
    public void UnequipItem(ItemData_Equipment _itemToRemove)
    {
        if (equipmentDictionary.TryGetValue(_itemToRemove, out InventoryItem value))
        {
            
            equipment.Remove(value);
            equipmentDictionary.Remove(_itemToRemove);
            _itemToRemove.RemoveModifiers();
        }
    }
   
    // Update Slot
    private void UpdateSlotUI()
    {
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
            {
                if (item.Key.equipmentType == equipmentSlot[i].equipmentType)
                    equipmentSlot[i].UpdateSlot(item.Value);
            }
        }

        for (int i = 0; i < inventorySlot.Length; i++)
        {
            inventorySlot[i].ClearSlot();
        }

        for (int i = 0; i < stashSlot.Length; i++)
        {
            stashSlot[i].ClearSlot();
        }

        for (int i = 0; i < inventory.Count; i++)
        {
            inventorySlot[i].UpdateSlot(inventory[i]);
        }

        for (int i = 0; i < stash.Count; i++)
        {
            stashSlot[i].UpdateSlot(stash[i]);
        }

        for (int i = 0; i < statSlot.Length; i++)
        {
            statSlot[i].UpdateStatValueUI();
        }
    }

    // Add Item
    public void AddItem(ItemData _item)
    {
        if (_item.itemType == ItemType.Equipment && CanAddItem())        
            AddInventory(_item);        
        else if (_item.itemType == ItemType.Material)        
            AddStash(_item);
        
        UpdateSlotUI();
    }

    public bool CanAddItem()
    {
        if(inventory.Count > inventorySlot.Length)
            return false;

        return true;
    }


    //Add Stash
    private void AddStash(ItemData _item)
    {
        if (stashDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            stash.Add(newItem);
            stashDictionary.Add(_item, newItem);
        }
    }

    //Add Inventory
    private void AddInventory(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            inventory.Add(newItem);
            inventoryDictionary.Add(_item, newItem);
        }
    }

    // RemoveItem
    public void RemoveItem(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            if (value.stackSize <= 1)
            {
                inventory.Remove(value);
                inventoryDictionary.Remove(_item);
            }
            else
            {
                value.RemoveStack();
            }
        }

        if (stashDictionary.TryGetValue(_item, out InventoryItem stashValue))
        {
            if (stashValue.stackSize <= 1)
            {
                stash.Remove(stashValue);
                stashDictionary.Remove(_item);
            }
            else
            {
                stashValue.RemoveStack();
            }
        }



        UpdateSlotUI();
    }

    // Craft
    public bool CanCraft(ItemData_Equipment _itemToCraft, List<InventoryItem> _requiredMaterials)
    {
        List<InventoryItem> materialsToRemove = new List<InventoryItem>();

        for (int i = 0; i < _requiredMaterials.Count; i++)
        {
            if (stashDictionary.TryGetValue(_requiredMaterials[i].data, out InventoryItem stashValue))
            {
                if (stashValue.stackSize < _requiredMaterials[i].stackSize)
                {
                    Debug.Log("Not Enough Materials");
                    return false;
                }
                else
                {
                    materialsToRemove.Add(stashValue);
                }
            }
            else
            {
                Debug.Log("Not Enough Materials");
                return false;
            }
        }

        for (int i = 0; i < materialsToRemove.Count; i++)
        {
            RemoveItem(materialsToRemove[i].data);

        }

        AddItem(_itemToCraft);
        Debug.Log("Here Is Your Item" + _itemToCraft.name);

        return true;
    }

    public List<InventoryItem> GetEquipmentList() => equipment;
    public List<InventoryItem> GetStashList() => stash;
    public ItemData_Equipment GetEquipment(EquipmentType _type)
    {
        ItemData_Equipment equipedItem = null;

        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == _type)
                equipedItem = item.Key;
        }

        return equipedItem;
    }

    //Use Flask
    public void UseHealFlask()
    {
        ItemData_Equipment currentHealFlask = GetEquipment(EquipmentType.HealFlask);

        if (currentHealFlask == null)
            return;

        bool canUseFlask = Time.time > lastUseTimeOfFlusk + flaskCooldown;

        if (canUseFlask)
        {
            flaskCooldown = currentHealFlask.itemCooldown;
            currentHealFlask.Effect(null);
            lastUseTimeOfFlusk = Time.time;
        }
        else
            Debug.Log("Effect On Cooldown");
    }

    public void UseMagicFlask()
    {
        ItemData_Equipment currentMagicFlask = GetEquipment(EquipmentType.MagicFlask);

        if (currentMagicFlask == null)
            return;

        bool canUseFlask = Time.time > lastUseTimeOfFlusk + flaskCooldown;

        if (canUseFlask)
        {
            flaskCooldown = currentMagicFlask.itemCooldown;
            currentMagicFlask.Effect(null);
            lastUseTimeOfFlusk = Time.time;
        }
        else
            Debug.Log("Effect On Cooldown");
    }



}

