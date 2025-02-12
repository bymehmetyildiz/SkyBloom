using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Player player;

    public int currency;

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
            return false;
        }


        currency -= _price;
        return true;
    }

}
