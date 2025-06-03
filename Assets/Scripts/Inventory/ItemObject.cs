using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class ItemObject : MonoBehaviour
{
   
    [SerializeField] private ItemData itemData;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 velocity;
    

    private void SetUpVisuals()
    {
        if (itemData == null)
            return;

        GetComponent<SpriteRenderer>().sprite = itemData.itemIcon;
        gameObject.name = itemData.itemName;
    }

    public void SetupItem(ItemData _itemData, Vector2 _velocity)
    {
        itemData = _itemData;
        rb.velocity = _velocity;
        SetUpVisuals();
    }


    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.M))
        //    rb.velocity = velocity;
    }

    public void PickUpItem()
    {
        if (itemData.itemType == ItemType.Currency)
        {
            ItemData_Currency item = (ItemData_Currency)itemData;
            item.Effect(null);
            StartCoroutine(UI_Controller.instance.ShowAdIcon());
            Destroy(gameObject);
            return;
        }

        if (!Inventory.instance.CanAddItem() && itemData.itemType == ItemType.Equipment)
        {
            rb.velocity = Vector2.up * 7;
            return;
        }

        if (itemData is ItemData_Equipment equipmentData)
        {
            if (equipmentData.equipmentType == EquipmentType.HealFlask ||
                equipmentData.equipmentType == EquipmentType.MagicFlask)
            {
                AudioManager.instance.PlaySFX(13, this.transform);
            }
            else
            {
                AudioManager.instance.PlaySFX(14, this.transform);
            }
        }


        Inventory.instance.AddItem(itemData);
        Destroy(gameObject);
    }
}
