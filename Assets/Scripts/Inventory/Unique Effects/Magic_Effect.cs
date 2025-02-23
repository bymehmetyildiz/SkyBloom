using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item Effect/Magic Effect")]
public class Magic_Effect : ItemEffect
{
    [Range(0f, 1f)]
    [SerializeField] private float magicPercent;

    public override void ExecuteEffect(Transform _enemyPosition)
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        int magicAmount = Mathf.RoundToInt(playerStats.maxMagic.GetValue() * magicPercent);

        playerStats.IncreaseMagic(magicAmount);

        UI_InGame.instance.UpdateMagic();
    }
}
