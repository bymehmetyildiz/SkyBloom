using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AnimationTrigger : MonoBehaviour
{
    private Enemy enemy => GetComponentInParent<Enemy>();

    //Call trigger Called
    public void AnimationTrigger()
    {
        enemy.AnimationTrigger();        
    }

    // Trigger Damage
    public void TriggerDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckDistance);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetComponent<Player>() != null)
            {

                PlayerStats playerStats = colliders[i].GetComponent<PlayerStats>();
                if (playerStats.isDead)
                    return;

                enemy.entityStats.DoDamage(playerStats);
            }
        }
    }

    // Counter Attack
    private void StartCounterAttack() => enemy.StartCounterAttack();
    
    private void StopCounterAttack() => enemy.StopCounterAttack();

    // Sp Attack (Mauler)
    private void CanExpand() => enemy.CanExpand();

    // Screen Shake
    private void ScreenShake() => enemy.fx.ScreenShake();
    
    // Projectile (Ranged Enemies)
    private void ReleaseProjectile() => enemy.ReleaseProjectile();

    // Teleport (Ivy)
    private void Relocate()
    {      
        if (enemy is Ivy)
        {
            Ivy ivy = (Ivy)enemy;
            ivy.FindPosition();
        }
    }

    private void MakeInvisible() => enemy.fx.MakeTransparent(true);
    private void MakeVisible() => enemy.fx.MakeTransparent(false);

    // Create Branch (Ivy)
    private void CreateBranch()
    {
        if (enemy is Ivy)
        {
            Ivy ivy = (Ivy)enemy;
            ivy.CreateBranch();
        }
    }

    // Create Trap (Ivy)
    private void CreateBall()
    {
        if (enemy is Ivy)
        {
            Ivy ivy = (Ivy)enemy;
            ivy.CreateBall();
        }
    }

    // Create Spell (Grim)
    private void CreateSpell()
    {
        if (enemy is Grim)
        {
            Grim grim = (Grim)enemy;
            grim.CreateSpell();
        }

    }



}
