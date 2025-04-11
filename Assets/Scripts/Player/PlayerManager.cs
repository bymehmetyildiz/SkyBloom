using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, ISaveManager
{
    public static PlayerManager instance;
    public Player player;

    public int currency = 0;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    public bool HasEnoughMoney(int _price)
    {
        if (_price > currency)
        {
            Debug.Log("Not Enough Money !");
        }
        return currency >= _price;
    }

    public void SpendCurrency(int _price) => currency -= _price;

    public int GetCurrency() => currency;

    public void LoadData(GameData _data)
    {
        if (_data != null)
            this.currency = _data.currency;
        else
            this.currency = 0;
    }

    public void SaveData(ref GameData _data)
    {
        _data.currency = this.currency;
    }
}
