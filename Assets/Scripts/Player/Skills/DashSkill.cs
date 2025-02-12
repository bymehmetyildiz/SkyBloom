using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class DashSkill : Skill
{
    public override void UseSkill()
    {
        base.UseSkill();
    }

    protected override void Start()
    {
        base.Start();
        
    }
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    protected override void Update()
    {
        base.Update();        
    }

    public override bool IsSkillUnlocked()
    {
        return base.IsSkillUnlocked();
    }
}
