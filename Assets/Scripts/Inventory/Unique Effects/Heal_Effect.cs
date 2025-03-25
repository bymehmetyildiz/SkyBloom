using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item Effect/Heal Effect")]
public class Heal_Effect : ItemEffect
{
    [Range(0f, 1f)]
    public float percent;
    public override void ExecuteEffect(Transform _enemyPosition)
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        int healAmount = Mathf.RoundToInt(playerStats.GetMaxHealth() * percent);

        playerStats.IncreaseHealth(healAmount);
    }
}
