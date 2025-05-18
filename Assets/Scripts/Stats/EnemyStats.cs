using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyStats : EntityStats
{
    private Enemy enemy;
    private ItemDrop dropSystem;
    

    [Header("Level Details")]
    [SerializeField] private int level = 1;

    [Range(0f, 1f)]
    [SerializeField] private float percentageModifier = 0.4f;

    protected override void Start()
    {
        ApplyModifiers();

        base.Start();
        enemy = GetComponent<Enemy>();
        dropSystem = GetComponent<ItemDrop>();
    }

    private void ApplyModifiers()
    {
        //Modify(strength);
        //Modify(agility);
        //Modify(intelligence);
        //Modify(vitality);

        Modify(damage);
        Modify(critChance);
        Modify(critPower);

        Modify(maxHealth);
        Modify(maxMagic);
        Modify(armor);
        Modify(evasion);
        Modify(magicResistance);

        Modify(fireDamage);
        Modify(iceDamage);
        Modify(shockDamage);
        
    }

    private void Modify(Stat _stat)
    {
        for (int i = 1; i < level; i++)
        {
            float modifier = _stat.GetValue() * percentageModifier;
            _stat.AddModifier(Mathf.RoundToInt(modifier));
        }
    }


    public override void DoDamage(EntityStats _entityStats)
    {
        base.DoDamage(_entityStats);
    }

    public override void TakeDamage(int _damage)
    {
        if (!enemy.canBeDamaged)
            return;

        base.TakeDamage(_damage);        
        enemy.DamageEffect();
        AudioManager.instance.PlaySFX(6, null);
    }

    public override void Dead()
    {
        base.Dead();
        dropSystem.GenerateDrop();
        enemy.Dead();
        Destroy(gameObject, 3.0f);


    }

    public override void DoMagicalDamage(EntityStats _entityStats)
    {
        base.DoMagicalDamage(_entityStats);
    }
}
