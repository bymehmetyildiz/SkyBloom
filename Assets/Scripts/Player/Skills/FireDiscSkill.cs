using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDiscSkill : Skill
{
    [Header("Skill Variables")]
    [SerializeField] private GameObject fireDisc;
    [SerializeField] private GameObject hitEffect;
    private int direction;
    [SerializeField] private int speed;
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override bool IsSkillUnlocked()
    {
        return base.IsSkillUnlocked();
    }

    public override void UseSkill()
    {
        base.UseSkill();

        if(IsSkillUnlocked())
        StartCoroutine(UseFireDiscSkill());
       
    }

    private IEnumerator UseFireDiscSkill()
    {
        yield return new WaitForSeconds(0.25f);
        direction = player.facingDir;
        GameObject newFireDisc = Instantiate(fireDisc, new Vector2((player.transform.position.x + xOffset * player.facingDir), 
            (player.transform.position.y + yOffset)), Quaternion.identity);
        FireDisc _fireDisc = newFireDisc.GetComponent<FireDisc>();
        _fireDisc.Setup(direction, speed, hitEffect, player);
        
    }

}
