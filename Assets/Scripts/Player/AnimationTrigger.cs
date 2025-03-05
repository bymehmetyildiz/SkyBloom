using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    
    [HideInInspector] public Player player;
    [SerializeField] private Animator attackEffectAnimator;
    [SerializeField] private Animator dashEffectAnimator;
    [SerializeField] private Animator flurrySlashAnimator;
    [SerializeField] private Animator aerialSlamAnimator;

    private void Start()
    {
        player = GetComponentInParent<Player>();

    }

    public void TriggerLand()
    {
        player.landTrigger = true;
      
    }

    public void AnimTrigger()
    {
        player.AnimationTrigger();
        attackEffectAnimator.SetBool("Attack", false);
        dashEffectAnimator.SetBool("Dash", false);
        flurrySlashAnimator.SetBool("Flurry", false);       
    }

    public void EffectAnimTrigger()
    {
        attackEffectAnimator.SetBool("Attack", true);
        attackEffectAnimator.SetInteger("ComboCounter", player.comboCounter);
    }

    public void TriggerAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckDistance);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetComponent<Enemy>() != null)
            {
                var enemy = colliders[i].GetComponent<Enemy>();
                var enemyStats = colliders[i].GetComponent<EnemyStats>();

                if (enemy.canBeDamaged == false)
                    return;

                StartCoroutine(DelayedDamage(enemyStats, enemy));
               
                
            }

        }
    }

    IEnumerator DelayedDamage(EnemyStats enemyStats, Enemy enemy)
    {
        yield return new WaitForSeconds(0.05f); // Small delay
        player.stunTrigger = true;
        if (!enemy.canBeDamaged) yield break; // Double-check
        player.entityStats.DoDamage(enemyStats);
        enemyStats.isDamaged = true;
        Inventory.instance.GetEquipment(EquipmentType.Weapon)?.Effect(enemyStats.transform);
    }

    public void ThrowSword()
    {
        SkillManager.instance.swordSkill.CreateSword();
    }

    public void ThunderTrigger() => player.skillManager.thunderSkill.SpawnBlackHole();   
    public void DashAttackTrigger() => dashEffectAnimator.SetBool("Dash", true);
    public void FlurrySlashTrigger() => flurrySlashAnimator.SetBool("Flurry", true); 
    public void AerialSlam()
    {
        aerialSlamAnimator.SetBool("Slam", true);
        player.skillManager.aerialSlamSkill.AerialSlamHit();

    }
    public void AerialSlamEnd() => aerialSlamAnimator.SetBool("Slam", false);
    public void EarthSlam() => player.skillManager.earthSlamSkill.CreateExplosion();
}
