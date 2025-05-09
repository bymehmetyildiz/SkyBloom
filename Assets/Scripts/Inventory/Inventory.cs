using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Inventory : MonoBehaviour, ISaveManager
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

    [Header("Data Base")]        
    public List<InventoryItem> loadedItems;
    public List<InventoryItem> loadedEquipment;

    private int newStackSize = 0;

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

        SaveManager.instance.LoadGame();

        StartItems();
    }
    #region StartItems
    
    private void StartItems()
    {
        if (loadedEquipment != null)
        {
            //Process equipment items
            foreach (InventoryItem item in loadedEquipment)
            {
                newStackSize = item.stackSize;
                EquipItem(item.data);
            }
        }
        if (SaveManager.instance.HasSavedData())
        {
            foreach (InventoryItem item in loadedItems)
            {
                // Create a new inventory item with the correct stack size
                InventoryItem newItem = new InventoryItem(item.data);
                newItem.stackSize = item.stackSize;

                // Add directly to inventory and dictionary
                inventory.Add(newItem);
                inventoryDictionary.Add(item.data, newItem);
            }
        }
        else
        {
            // No loaded items, use starting items
            for (int i = 0; i < startingItems.Count; i++)
            {
                if (startingItems[i] != null)
                    AddItem(startingItems[i]);
            }
        }


        // Update the UI
        UpdateSlotUI();
    }
    
    #endregion

   

    public void EquipItem(ItemData _item)
    {
        ItemData_Equipment newEquipment = _item as ItemData_Equipment;

        // Special handling for flasks - transfer entire stack
        if (newEquipment.equipmentType == EquipmentType.HealFlask ||
            newEquipment.equipmentType == EquipmentType.MagicFlask)
        {
            // Get current stack size before removing from inventory            
            if (inventoryDictionary.TryGetValue(_item, out InventoryItem inventoryItem))
            {
                newStackSize = inventoryItem.stackSize;
            }

            // Check for existing equipment of same type
            ItemData_Equipment oldEquipment = null;
            InventoryItem oldEquipmentItem = null;

            foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
            {
                if (item.Key.equipmentType == newEquipment.equipmentType)
                {
                    oldEquipment = item.Key;
                    oldEquipmentItem = item.Value;
                    break;
                }
            }

            // If there's existing equipment, move it back to inventory with its full stack
            if (oldEquipment != null)
            {
                int oldStackSize = oldEquipmentItem.stackSize;

                // Remove from equipment
                UnequipItem(oldEquipment);

                // Add back to inventory with full stack
                InventoryItem returnedItem = new InventoryItem(oldEquipment);
                returnedItem.stackSize = oldStackSize;

                // Remove any existing entry of the same item
                if (inventoryDictionary.ContainsKey(oldEquipment))
                {
                    inventory.Remove(inventoryDictionary[oldEquipment]);
                    inventoryDictionary.Remove(oldEquipment);
                }

                inventory.Add(returnedItem);
                inventoryDictionary.Add(oldEquipment, returnedItem);
            }

            // Create new equipment item with full stack
            InventoryItem newItem = new InventoryItem(newEquipment);
            newItem.stackSize = newStackSize;

            // Add to equipment
            equipment.Add(newItem);
            equipmentDictionary.Add(newEquipment, newItem);
            newEquipment.AddModifiers();

            UI_Potions.instance.AssignPotions(newEquipment.itemIcon, newItem.stackSize.ToString(), newEquipment.equipmentType);

            // Remove entire stack from inventory
            if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
            {
                inventory.Remove(value);
                inventoryDictionary.Remove(_item);
            }
        }
        else
        {
            // Original equipment handling code for non-flask items
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

            UI_Potions.instance.AssignPotions(newEquipment.itemIcon, newItem.stackSize.ToString(), newEquipment.equipmentType);

            RemoveItem(_item);
        }

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

        UI_Potions.instance.RemovePotions(_itemToRemove.equipmentType);
    }
   
    // Update Slot
    public void UpdateSlotUI()
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
   
    // Use Flasks
    public void UseHealFlask()
    {
        ItemData_Equipment currentHealFlask = GetEquipment(EquipmentType.HealFlask);

        if (currentHealFlask == null)
            return;

        bool canUseFlask = Time.time > lastUseTimeOfFlusk + flaskCooldown;

        // Check if player can be healed (not at full health)
        EntityStats playerStats = PlayerManager.instance.player.stats;
        bool canBeHealed = playerStats.currentHealth < playerStats.maxHealth.GetValue();

        if (canUseFlask && canBeHealed)
        {
            if (equipmentDictionary.TryGetValue(currentHealFlask, out InventoryItem flaskItem))
            {
                flaskCooldown = currentHealFlask.itemCooldown;
                currentHealFlask.Effect(null);
                lastUseTimeOfFlusk = Time.time;

                // Decrease stack and remove if empty
                flaskItem.RemoveStack();

                if (flaskItem.stackSize <= 0)
                {
                    // First clear the UI
                    UI_Potions.instance.RemovePotions(currentHealFlask.equipmentType);

                    // Then remove from equipment
                    UnequipItem(currentHealFlask);

                    // Clear the slot UI
                    foreach (UI_EquipmentSlot slot in equipmentSlot)
                    {
                        if (slot.equipmentType == currentHealFlask.equipmentType)
                        {
                            slot.ClearSlot();
                            break;
                        }
                    }
                }
                else
                {
                    // Update UI only if potion still has charges
                    UI_Potions.instance.AssignPotions(currentHealFlask.itemIcon, flaskItem.stackSize.ToString(), currentHealFlask.equipmentType);
                }

                UpdateSlotUI();
            }
        }
        else if (!canBeHealed)
        {
            Debug.Log("Health is already full!");
        }
        else
        {
            Debug.Log("Effect On Cooldown");
        }
    }

    public void UseMagicFlask()
    {
        ItemData_Equipment currentMagicFlask = GetEquipment(EquipmentType.MagicFlask);

        if (currentMagicFlask == null)
            return;

        bool canUseFlask = Time.time > lastUseTimeOfFlusk + flaskCooldown;

        // Check if player can restore magic (not at full magic)
        EntityStats playerStats = PlayerManager.instance.player.stats;
        bool canRestoreMagic = playerStats.currentMagic < playerStats.maxMagic.GetValue();

        if (canUseFlask && canRestoreMagic)
        {
            if (equipmentDictionary.TryGetValue(currentMagicFlask, out InventoryItem flaskItem))
            {
                flaskCooldown = currentMagicFlask.itemCooldown;
                currentMagicFlask.Effect(null);
                lastUseTimeOfFlusk = Time.time;

                // Decrease stack and remove if empty
                flaskItem.RemoveStack();

                if (flaskItem.stackSize <= 0)
                {
                    // First clear the UI
                    UI_Potions.instance.RemovePotions(currentMagicFlask.equipmentType);

                    // Then remove from equipment
                    UnequipItem(currentMagicFlask);

                    // Clear the slot UI
                    foreach (UI_EquipmentSlot slot in equipmentSlot)
                    {
                        if (slot.equipmentType == currentMagicFlask.equipmentType)
                        {
                            slot.ClearSlot();
                            break;
                        }
                    }
                }
                else
                {
                    // Update UI only if potion still has charges
                    UI_Potions.instance.AssignPotions(currentMagicFlask.itemIcon, flaskItem.stackSize.ToString(), currentMagicFlask.equipmentType);
                }

                UpdateSlotUI();
            }
        }
        else if (!canRestoreMagic)
        {
            Debug.Log("Magic is already full!");
        }
        else
        {
            Debug.Log("Effect On Cooldown");
        }
    }
    #region Save/Load Data
    
    public void LoadData(GameData _data)
    {
        foreach (KeyValuePair<string, int> pair in _data.inventory)
        {
            foreach (var item in GetItemDataBase())
            {
                if (item != null && item.itemId == pair.Key)
                {
                    InventoryItem itemToLoad = new InventoryItem(item);
                    itemToLoad.stackSize = pair.Value;

                    loadedItems.Add(itemToLoad);
                }

            }
        }

        foreach (KeyValuePair<string, int> pair in _data.equipment)
        {
            foreach (var item in GetItemDataBase())
            {
                if (item != null && item.itemId == pair.Key)
                {
                    InventoryItem itemToLoad = new InventoryItem(item);
                    itemToLoad.stackSize = pair.Value;
                   
                    loadedEquipment.Add(itemToLoad);
                }
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        _data.inventory.Clear();
        _data.equipment.Clear();

        foreach (KeyValuePair<ItemData, InventoryItem> pair in inventoryDictionary)
        {
            _data.inventory.Add(pair.Key.itemId, pair.Value.stackSize);
        }

        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> pair in equipmentDictionary)
        {
            _data.equipment.Add(pair.Key.itemId, pair.Value.stackSize);
            
        }

    }

    private List<ItemData> GetItemDataBase()
    {
        List<ItemData> itemDataBase = new List<ItemData>();
        string[] assetNames = AssetDatabase.FindAssets("", new[] { "Assets/Data/Equipment" });

        foreach (string SOName in assetNames) // SOName = Scriptable Object Name;
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var itemData = AssetDatabase.LoadAssetAtPath<ItemData>(SOpath);
            itemDataBase.Add(itemData);
        }

        return itemDataBase;
    }
    
    #endregion 
   
}