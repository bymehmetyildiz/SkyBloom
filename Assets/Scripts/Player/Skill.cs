using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{

    public float cooldown;
    protected float cooldownTimer;
    public int magicAmount;

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
        if(cooldownTimer < 0 && player.stats.currentMagic > magicAmount)
        {
            cooldownTimer = cooldown;
            return true;
        }
        return false;
    }

    public virtual bool IsSkillUnlocked()
    {
        if (skillButton.unlocked)
        {
            return true;
        }

        return false;
    }


    public virtual void UseSkill()
    {
        if (player.stats.currentMagic >= magicAmount)
        {
            player.stats.DecreaseMagic(magicAmount);
            UI_InGame.instance.UpdateMagic();
        }
        else
        {
            Debug.Log("Not Enough Magic");
            return;
        }

    }

  




}
