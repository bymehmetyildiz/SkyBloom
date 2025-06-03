using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Currency")]
public class ItemData_Currency : ItemData
{
    public int amount;

    public void ExecuteEffect()
    {
        PlayerManager.instance.currency += amount;
    }

    
}
