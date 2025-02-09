using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{

    [SerializeField] protected float cooldown;
    protected float cooldownTimer;

    protected Player player;

    [Header("Unlock")]
    
    [SerializeField] private UI_SkillTreeSlot skillButton;

    protected virtual void Start()
    {
        player = PlayerManager.instance.player;
       
    }

    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        if(cooldownTimer < 0 )
        {
            cooldownTimer = cooldown; 
            UseSkill();
            return true;
        }
        return false;
    }

    public virtual bool IsSkillUnlocked()
    {
        if(skillButton.unlocked)
            return true;

        return false;
    }


    public virtual void UseSkill()
    {
        // Use Skill
        Debug.Log("Skill Used");
    }

  




}
