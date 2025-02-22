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

        Inventory.instance.UnequipItem(item.data as ItemData_Equipment);
        Inventory.instance.AddItem(item.data as ItemData_Equipment);
        ClearSlot();


        UI_InGame.instance.UpdateHealth();
    }

   
}
