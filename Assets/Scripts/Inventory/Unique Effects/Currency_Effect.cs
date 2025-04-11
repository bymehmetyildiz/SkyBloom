using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item Effect/Currency")]
public class Currency_Effect : ItemEffect
{    
    public int amount = 1;

    public override void ExecuteEffect(Transform _enemyPosition)
    {
        PlayerManager.instance.currency += amount;
    }
}
