using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialSlamSkill : Skill
{
    [SerializeField] private GameObject aerialSlamHit;
    [SerializeField] private Transform hitPosition;

    public void AerialSlamHit()
    {
        if (!IsSkillUnlocked())
            return;

        GameObject aerialSlam = Instantiate(aerialSlamHit, hitPosition.position, Quaternion.identity);
        aerialSlam.transform.localScale = Vector3.one * 2;
    }

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override bool IsSkillUnlocked()
    {
        return base.IsSkillUnlocked();
    }
}
