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

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

        if (_damage > GetMaxHealth() * 0.3f)
            player.SetupKnockbackPower(new Vector2(7, 10));

        player.DamageEffect();

    }

    protected override void Dead()
    {
        base.Dead();
        player.Dead();

        //GetComponent<PlayerItemDrop>()?.GenerateDrop();
    }

    protected override void DecreaseHealth(int _damage)
    {
        base.DecreaseHealth(_damage);
        
    }
}
