using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CrazyGames;

public class UI_CurrencyManager : MonoBehaviour
{
    [SerializeField] private TMP_Text crystalAmountText;

    private void Start()
    {
        UpdateCurrency();
    }

    private void Update()
    {
        UpdateCurrency();
    }

    public void UpdateCurrency()
    {
        crystalAmountText.text = PlayerManager.instance.currency.ToString();
    }

}
