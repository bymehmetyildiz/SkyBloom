using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemDrop : ItemDrop
{
    [Header("Player's Drop")]
    [SerializeField] private float chanseToDropEquipment;
    [SerializeField] private float chanseToDropItem;

    public override void GenerateDrop()
    {
        Inventory inventory = Inventory.instance;
       
        List<InventoryItem> itemsToUnequip = new List<InventoryItem>();
        List<InventoryItem> itemsToLoose = new List<InventoryItem>();

        foreach (InventoryItem item in inventory.GetEquipmentList())
        {
            if (Random.Range(0, 100) <= chanseToDropEquipment)
            {
                DropItem(item.data);
                itemsToUnequip.Add(item);
            }
        }

        for (int i = 0; i < itemsToUnequip.Count; i++)
        {
            inventory.UnequipItem(itemsToUnequip[i].data as ItemData_Equipment);
        }

        foreach (InventoryItem item in inventory.GetStashList())
        {
            if (Random.Range(0, 100) <= chanseToDropItem)
            {
                DropItem(item.data);
                itemsToLoose.Add(item);
            }
        }

        for (int i = 0; i < itemsToLoose.Count; i++)
        {
            inventory.RemoveItem(itemsToLoose[i].data);
        }
    }
}
