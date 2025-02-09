using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public DashSkill dashSkill {get; private set;}
    public SwordSkill swordSkill { get; private set;}
    public SwordRainSkill rainSkill { get; private set;}
    public ThunderSkill thunderSkill { get; private set;}
    public FireDiscSkill fireDiscSkill { get; private set;}
    public EarthSlamSkill earthSlamSkill { get; private set;}
    public FlurrySlashSkill flurrySlashSkill { get; private set;}
    public TwisterSkill twisterSkill { get; private set;}
    public SwordRainSkill swordRainSkill { get; private set;}
    public AerialSlamSkill aerialSlamSkill { get; private set;}
    

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        dashSkill = GetComponent<DashSkill>();
        swordSkill = GetComponent<SwordSkill>();
        rainSkill = GetComponent<SwordRainSkill>();
        thunderSkill = GetComponent<ThunderSkill>();
        fireDiscSkill = GetComponent<FireDiscSkill>();
        earthSlamSkill = GetComponent<EarthSlamSkill>();
        flurrySlashSkill = GetComponent<FlurrySlashSkill>();
        twisterSkill = GetComponent<TwisterSkill>();
        swordRainSkill = GetComponent<SwordRainSkill>();
        aerialSlamSkill = GetComponent <AerialSlamSkill>();
    }
}
