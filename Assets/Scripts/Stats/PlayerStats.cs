using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : EntityStats
{
    private Player player;

    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }

    public override void TakeDamage(int _damage, int _damageDirection)
    {
        base.TakeDamage(_damage, _damageDirection);
        player.DamageEffect(_damageDirection);
    }

    protected override void Dead()
    {
        base.Dead();
        player.Dead();

        GetComponent<PlayerItemDrop>()?.GenerateDrop();
    }
}
