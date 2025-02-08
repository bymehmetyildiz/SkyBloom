using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSlamSkill : Skill
{
    [SerializeField] private GameObject explosionTransformPrefab;
    [SerializeField] private GameObject explosionPrefab;    
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;    
    [SerializeField] private float speed;
    [SerializeField] private float expTimer;

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public void CreateExplosion()
    {
        GameObject explosion = Instantiate(explosionTransformPrefab, player.transform.position + Vector3.right * player.facingDir, Quaternion.identity);
        EarthSlamExplosion earthSlamExplosion = explosion.GetComponent<EarthSlamExplosion>();
        earthSlamExplosion.SetupEarthSlam(groundCheckDistance, whatIsGround, cooldown, speed,explosionPrefab, expTimer);

        if(earthSlamExplosion != null)
            StartCoroutine(earthSlamExplosion.CrateExplosion());
    }



}
