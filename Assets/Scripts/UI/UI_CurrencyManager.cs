using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CrazyGames;

public class UI_CurrencyManager : MonoBehaviour
{
    [SerializeField] private TMP_Text crystalAmountText;


    
    void Update()
    {
        crystalAmountText.text = PlayerManager.instance.currency.ToString();
    }

    
}
