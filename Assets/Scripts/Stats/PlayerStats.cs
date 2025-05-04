using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : EntityStats
{
    public Player player;

    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        isDamaged = true;
        if (_damage > GetMaxHealth() * 0.3f)
            player.SetupKnockbackPower(new Vector2(7, 10));        
        player.DamageEffect();
        player.StartCoroutine(IsDamagedDur());
    }

    private IEnumerator IsDamagedDur()
    {
        yield return new WaitForSeconds(0.2f);
        isDamaged = false;
    }


    public override void Dead()
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
