using UnityEngine;

public class EntityStats : MonoBehaviour
{
    private EntityFX fx;

    [Header("Major Stats")]
    public Stat strength; // increase damage and crit chance
    public Stat agility; // increase evasion
    public Stat intelligence; // increase magic damage
    public Stat vitality; // increase health

    [Header("Ofensive Stats")]
    public Stat damage;
    public Stat critChance;
    public Stat critPower;  //Default 150%

    [Header("Defensive Stats")]
    public Stat maxHealth;
    public Stat armor;
    public Stat evasion;
    public Stat magicResistance;

    [Header("Magic Stats")]
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat shockDamage;
    [SerializeField] private float ailmentDur = 4;

    public bool isIgnited; // Do damage frequently
    public bool isFrozen; // -20 armor
    public bool isShocked; // -20 evasion

    // Ignite Vars
    private float ignitionTimer;
    private float freezeTimer;
    private float shockTimer;

    private float ignitionDamageTimer;
    private float ignitionDamageCooldown = 0.3f;
    private int ignitionDamage;

    public int currentHealth;
    public bool isDead { get; private set; }

    public System.Action onHealthChanged;
    public bool isInvincible;

    protected virtual void Start()
    {
        critPower.SetDefaultValue(150);
        currentHealth = GetMaxHealth();
        fx = GetComponent<EntityFX>();
    }

    protected virtual void Update()
    {
        ignitionTimer -= Time.deltaTime;
        freezeTimer -= Time.deltaTime;
        shockTimer -= Time.deltaTime;

        ignitionDamageTimer -= Time.deltaTime;

        if (ignitionTimer < 0)
            isIgnited = false;

        if (freezeTimer < 0)
            isFrozen = false;

        if (shockTimer < 0)
            isShocked = false;

        if(isIgnited)
            ApplyIgnitionDamage();

    }

    private void ApplyIgnitionDamage()
    {
        if (ignitionDamageTimer < 0)
        {
            DecreaseHealth(ignitionDamage);

            if (currentHealth <= 0 && !isDead)
                Dead();

            ignitionDamageTimer = ignitionDamageCooldown;
        }
    }

    // Do Damage
    public virtual void DoDamage(EntityStats _entityStats)
    {
        if (CanAvoidAttack(_entityStats))
            return;

        _entityStats.GetComponent<Entity>().SetupKnockBackDir(transform);

        int totalDamage = damage.GetValue();        

        if (CanCritAttack())
        {
            totalDamage = CriticalDamage(totalDamage);
        }

        fx.CreateHitFX(_entityStats.transform);

        totalDamage = CheckTargetArmor(_entityStats, totalDamage);
        _entityStats.TakeDamage(totalDamage);        
        _entityStats.TakeDamage(totalDamage);        
    }

    // Do Magic Damage
    public virtual void DoMagicalDamage(EntityStats _entityStats)
    {
        _entityStats.GetComponent<Entity>().SetupKnockBackDir(transform);

        int _fireDamage = fireDamage.GetValue();
        int _iceDamage = iceDamage.GetValue();
        int _shockDamage = shockDamage.GetValue();

        int totalMagicDamage = _fireDamage + _iceDamage + _shockDamage + intelligence.GetValue();

        totalMagicDamage = CheckMagicResistance(_entityStats, totalMagicDamage);
        _entityStats.TakeDamage(totalMagicDamage);

        if (Mathf.Max(_fireDamage, _iceDamage, _shockDamage) <= 0)
            return;

        bool canApplyIgnite = _fireDamage > _iceDamage && _fireDamage > _shockDamage;
        bool canApplyIce = _iceDamage > _fireDamage && _iceDamage > _shockDamage;
        bool canApplyShock = _shockDamage > _fireDamage && _shockDamage > _iceDamage;


        while (!canApplyIgnite && !canApplyIce && !canApplyShock)
        {
            if (Random.value < 0.3f && _fireDamage > 0)
            {
                canApplyIgnite = true;
                _entityStats.ApplyAilments(canApplyIgnite, canApplyIce, canApplyShock);
                return;
            }

            if (Random.value < 0.5f && _iceDamage > 0)
            {
                canApplyIce = true;
                _entityStats.ApplyAilments(canApplyIgnite, canApplyIce, canApplyShock);
                return;
            }
            if (Random.value < 0.5f && _shockDamage > 0)
            {
                canApplyShock = true;
                _entityStats.ApplyAilments(canApplyIgnite, canApplyIce, canApplyShock);
                return;
            }

        }

        if (canApplyIgnite)
            _entityStats.IgnitionDamage(Mathf.RoundToInt(_fireDamage * 0.2f));

        _entityStats.ApplyAilments(canApplyIgnite, canApplyIce, canApplyShock);

    }

    //Check Magic Resistance
    private int CheckMagicResistance(EntityStats _entityStats, int totalMagicDamage)
    {
        totalMagicDamage -= _entityStats.magicResistance.GetValue() + (_entityStats.intelligence.GetValue() * 3);
        totalMagicDamage = Mathf.Clamp(totalMagicDamage, 0, int.MaxValue);
        return totalMagicDamage;
    }

    // Apply Ailments
    public void ApplyAilments(bool _ignite, bool _freeze, bool _shock)
    {
        if (isIgnited || isFrozen || isShocked)
            return;

        if(_ignite)
        {
            isIgnited = _ignite;
            ignitionTimer = ailmentDur;

            fx.IgnitionFxInvoke(ailmentDur);
        }
        if (_freeze)
        {
            freezeTimer = ailmentDur;
            isFrozen = _freeze;

            float slowPercentage = 0.2f;
            GetComponent<Entity>().FreezeEntity(slowPercentage, ailmentDur);

            fx.FreezeFxInvoke(ailmentDur);
        }
        if (_shock)
        {
            shockTimer = ailmentDur;
            isShocked = _shock;
            fx.ShockFxInvoke(ailmentDur);
        }
    }

    public virtual void IgnitionDamage(int _damage) => ignitionDamage = _damage;

    // Take Damage
    public virtual void TakeDamage(int _damage)
    {
        if (isInvincible)
            return;

        DecreaseHealth(_damage);

        if (currentHealth <= 0 && !isDead)
        {
            Dead();
        }
    }

    // Decrease Health
    protected virtual void DecreaseHealth(int _damage)
    {
        currentHealth -= _damage;

        if(onHealthChanged != null)
            onHealthChanged();
    }

    //Increase Health
    public virtual void IncreaseHealth(int _amount)
    {
        currentHealth += _amount;

        if(currentHealth > GetMaxHealth())
            currentHealth = GetMaxHealth();

        if(onHealthChanged != null)
            onHealthChanged();
    }

    // TODO Mana Bar ************************************************************

    // Die
    protected virtual void Dead()
    {
        isDead = true;
    }

    public void KillEntity()
    {
        if(!isDead)
            Dead();
    }
        
    //Invincible
    public void Invincible(bool _isInvincible) => isInvincible = _isInvincible;

    // Check Evasion
    private bool CanAvoidAttack(EntityStats _entityStats)
    {
        int totalEvasion = _entityStats.evasion.GetValue() + _entityStats.agility.GetValue();

        if (isShocked)
            totalEvasion += 20;

        if (Random.Range(0, 100) < totalEvasion)
        {
            return true;
        }
        return false;
    }

    // Check Armor
    private int CheckTargetArmor(EntityStats _entityStats, int totalDamage)
    {
        if(_entityStats.isFrozen)
            totalDamage -= Mathf.RoundToInt(_entityStats.armor.GetValue() * 0.8f);
        else
            totalDamage -= _entityStats.armor.GetValue();


        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }

    //Check Critical
    private bool CanCritAttack()
    {
        int totalCriticalChance = critChance.GetValue() + agility.GetValue();

        if (Random.Range(0, 100) <= totalCriticalChance)
        {
            return true;
        }
        return false;
    }

    // Critical Damage
    private int CriticalDamage(int _damage)
    {
        float totalCritPower = (critPower.GetValue() + agility.GetValue()) * 0.01f;
        float critDamage = _damage * totalCritPower;

        return Mathf.RoundToInt(critDamage);
    }

    // Max Health
    public int GetMaxHealth()
    {
        return maxHealth.GetValue() + vitality.GetValue() * 5;
    }

    public Stat GetStat(StatType _statType)
    {
        if (_statType == StatType.Strength) return strength;
        else if(_statType == StatType.Agility) return agility;
        else if(_statType == StatType.Vitality) return vitality;
        else if(_statType == StatType.Intelligence) return intelligence;
        else if(_statType == StatType.Damage) return damage;
        else if(_statType == StatType.CritChance) return critChance;
        else if(_statType == StatType.CritPower) return critPower;
        else if(_statType == StatType.Health) return maxHealth;
        else if(_statType == StatType.Armor) return armor;
        else if(_statType == StatType.Evasion) return evasion;
        else if(_statType == StatType.MagicRes) return magicResistance;
        else if(_statType == StatType.FireDamage) return fireDamage;
        else if(_statType == StatType.IceDamage) return iceDamage;
        else if(_statType == StatType.ShockDamage) return shockDamage;

        return null;
    }
}

public enum StatType
{
    Strength,
    Agility,
    Vitality,
    Intelligence,
    Damage,
    CritChance,
    CritPower,
    Health,
    Armor,
    Evasion,
    MagicRes,
    FireDamage,
    IceDamage,
    ShockDamage
}