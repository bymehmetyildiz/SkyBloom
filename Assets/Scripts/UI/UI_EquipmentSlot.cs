using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EquipmentSlot : UI_ItemSlot
{
    public EquipmentType equipmentType;

    public override void Start()
    {
        base.Start();
        
    }

    private void OnValidate()
    {
        gameObject.name = equipmentType.ToString();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (item == null || item.data == null)
            return;

        ItemData_Equipment equipment = item.data as ItemData_Equipment;

        // Special handling for flasks - transfer entire stack
        if (equipment.equipmentType == EquipmentType.HealFlask ||
            equipment.equipmentType == EquipmentType.MagicFlask)
        {
            // Store the stack size before unequipping
            int stackSize = item.stackSize;

            // Unequip the item
            Inventory.instance.UnequipItem(equipment);

            // Add the item to inventory with the original stack size
            InventoryItem newInventoryItem = new InventoryItem(equipment);
            newInventoryItem.stackSize = stackSize;

            // Remove existing entry if any
            if (Inventory.instance.inventoryDictionary.ContainsKey(equipment))
            {
                Inventory.instance.inventory.Remove(Inventory.instance.inventoryDictionary[equipment]);
                Inventory.instance.inventoryDictionary.Remove(equipment);
            }

            // Add new stack to inventory
            Inventory.instance.inventory.Add(newInventoryItem);
            Inventory.instance.inventoryDictionary.Add(equipment, newInventoryItem);
        }
        else
        {
            // Original behavior for non-flask equipment
            Inventory.instance.UnequipItem(equipment);
            Inventory.instance.AddItem(equipment);
        }

        ClearSlot();
        UI_InGame.instance.UpdateHealth();
        UI_InGame.instance.UpdateMagic();

        // Update UI
        Inventory.instance.UpdateSlotUI();
    }

}
