using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ItemToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI extraInfoText;

    public void ShowToolTip(ItemData_Equipment _item)
    {
        if (_item == null)
            return;

        itemNameText.text = _item.itemName;
        itemTypeText.text = _item.itemType.ToString();
        itemDescription.text = _item.GetDescription();

        // Show heal or magic restoration if applicable
        if (_item.equipmentType == EquipmentType.HealFlask && _item.itemEffect is Heal_Effect healEffect)        
            extraInfoText.text = $"Heals: %{healEffect.percent * 100} HP";        
        else if (_item.equipmentType == EquipmentType.MagicFlask && _item.itemEffect is Magic_Effect magicEffect)        
            extraInfoText.text = $"Restores: %{magicEffect.percent * 100} MP";        
        else        
            extraInfoText.text = "";
        

        gameObject.SetActive(true);
    }

    public void HideToolTip() => gameObject.SetActive(false);
}
