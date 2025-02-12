using UnityEngine;
using UnityEngine.UI;

public class SwordRainSkill : Skill
{
    [Header("Prefabs")] 
    public GameObject rainSwordPrefab;
    public GameObject rainStuckPrefab;
    public GameObject hitEffect;

    [Header("Variables")]
    public int defaultSword;
    private int amountOfSwords;
    public float rainDuration;
    private float rainTimer;
    public float rainSpeed;
    public float destroyDur;
    public bool canRain;
    private float randomX;
    private float screenTop;
    private float screenLeft;
    private float screenRight;
    public int defaultStuck;
    private int amountOfStucks;

    

    protected override void Start()
    {
        base.Start();

        amountOfSwords = defaultSword;
        amountOfStucks = defaultStuck;
    
    }

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
        
    }

    protected override void Update()
    {
        base.Update();

        rainTimer -= Time.deltaTime;

        if (IsSkillUnlocked())
            SwordRain();

    
    }


    private void SwordRain()
    {
        if (amountOfSwords > 0 && canRain)
        {
            screenLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
            screenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
            screenTop = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
            

            for (int i = 0; i < amountOfSwords; i++)
            {
                if (rainTimer <= 0)
                {
                    rainTimer = rainDuration;
                    randomX = Random.Range(screenLeft, screenRight);
                    GameObject newSwordDrop = Instantiate(rainSwordPrefab, new Vector2(randomX, screenTop), Quaternion.identity);                    
                    DropSwordContoller newSwordContoller = newSwordDrop.GetComponent<DropSwordContoller>();
                    newSwordContoller.SetUpRainSword(rainSpeed, this, destroyDur);
                    amountOfSwords--;
                }
            }

            if (amountOfSwords <= 0)
            {
                canRain = false;
                amountOfStucks = defaultStuck;
                amountOfSwords = defaultSword;
                
            }
        }
    }

    public void GroundStuckSword(Vector2 _stuckTransform)
    {
        if (amountOfStucks > 0)
        {
            Instantiate(rainStuckPrefab, _stuckTransform, Quaternion.identity);            
            amountOfStucks--;
        }
        

    }

    public void SpawnHitEffect(Vector2 hitPoint) => Instantiate(hitEffect, hitPoint, Quaternion.identity);

    public override bool IsSkillUnlocked()
    {
        return base.IsSkillUnlocked();
    }
}


