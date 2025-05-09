using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Potions : MonoBehaviour
{
    public static UI_Potions instance;
    [SerializeField] private Image healthIcon;
    [SerializeField] private Image magicIcon;
    [SerializeField] private TextMeshProUGUI healthPotionAmount;
    [SerializeField] private TextMeshProUGUI magicPotionAmount;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        if(healthIcon.sprite == null)
            healthIcon.gameObject.SetActive(false);
        if(magicIcon.sprite == null)
            magicIcon.gameObject.SetActive(false);
    }

    public void AssignPotions(Sprite _icon, string _amount, EquipmentType _equipmentType)
    {
        if (_equipmentType == EquipmentType.HealFlask)
        {
            if (healthIcon.gameObject.activeSelf == false)
                healthIcon.gameObject.SetActive(true);

            healthIcon.sprite = _icon;
            healthPotionAmount.text = _amount;
        }
        else if (_equipmentType == EquipmentType.MagicFlask)
        {
            if (magicIcon.gameObject.activeSelf == false)
                magicIcon.gameObject.SetActive(true);

            magicIcon.sprite = _icon;
            magicPotionAmount.text = _amount;
        }
        else
            return;
    }

    public void RemovePotions(EquipmentType _equipmentType)
    {
        if (_equipmentType == EquipmentType.HealFlask)
        {
            healthIcon.gameObject.SetActive(false);
        }
        else if (_equipmentType == EquipmentType.MagicFlask)
        {
            magicIcon.gameObject.SetActive(false);

        }
    }


}
