using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThunderSkill : Skill
{
    [Header("Thunder Skill Variables")]
    public GameObject blackHolePrefab;
    public GameObject spawnedBlackHole;
    public bool canGrow;
    public int damage;
    public float maxSize;
    public float growSpeed;
    public float castDelayTime;
    public float speed;
    public float offset;
   

    protected override void Start()
    {
        base.Start();

    }

    protected override void Update()
    {
        base.Update();
    }

    public void SpawnBlackHole()
    {
        GameObject newBlackHole = Instantiate(blackHolePrefab, player.transform.position, Quaternion.identity);
        spawnedBlackHole = newBlackHole;
        newBlackHole.GetComponent<ThunderSkillController>().SetUpThunderSkill(damage, maxSize, growSpeed, speed, offset, player);
        newBlackHole.GetComponent<ThunderSkillController>().canGrow = true;
        StartCoroutine(newBlackHole.GetComponent<ThunderSkillController>().StrikeThunder(castDelayTime));   
    }

    public bool IsBlackHoleSpawned()
    {
        if(spawnedBlackHole == null)
            return false;

        return true;
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
