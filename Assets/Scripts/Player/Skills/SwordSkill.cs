using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum SwordType
{
    None,
    Regular,
    Bounce,
    Pierce,
    Spin
}

public class SwordSkill : Skill, ISaveManager
{
    public SwordType swordType = SwordType.None;   
    private int typeCounter;
    private int unlockedTypes;
    [SerializeField] private UI_InGame inGameUI;


    [Header("Bounce Info")]
    [SerializeField] private UI_SkillTreeSlot bounceSkillButton;
    public bool isBounceUnlocked;
    [SerializeField] private int bounceAmount;
    [SerializeField] private float bounceGravity;

    [Header("Pierce Info")]
    [SerializeField] private UI_SkillTreeSlot pierceSkillButton;
    public bool isPierceUnlocked;
    [SerializeField] private int pierceAmount;
    [SerializeField] private float pierceGravity;

    [Header("Regular Info")]
    [SerializeField] private UI_SkillTreeSlot regularSkillButton;
    public bool isRegularUnlocked;
    [SerializeField] private GameObject swordPrefab;
    public Vector2 launchForce;
    public float swordGravity;
    [SerializeField] private float regularGravity;
    [SerializeField] private float freezeDur;
    [SerializeField] private float returnSpeed;

    [Header("Spin Info")]
    [SerializeField] private UI_SkillTreeSlot spinSkillButton;
    public bool isSpinUnlocked;
    [SerializeField] private float maxTravelDistance;
    [SerializeField] private float spinDuration;
    [SerializeField] private float spinGravity;
    [SerializeField] private float damageCooldown = 0.35f;

    [Header("Aim Dots")]
    [SerializeField] private int numberOfDots;
    public float spaceBetweenDots;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotParent;
    public GameObject[] dots;
    public Vector2 finalDir;

    protected override void Start()
    {
        
        base.Start();
        GenerateDots();
        SetupGravity();
     
        inGameUI.SwitchSwordIcon(typeCounter);

        CheckRegular();
        CheckPierce();
        CheckSpin();
        CheckBounce();
    }

    private void SetupGravity()
    {
        if (swordType == SwordType.Bounce)
            swordGravity = bounceGravity;
        else if(swordType == SwordType.Pierce)
            swordGravity = pierceGravity;
        else if(swordType == SwordType.Spin)
            swordGravity = spinGravity;
        else if(swordType== SwordType.Regular)
            swordGravity = regularGravity;
    }

    protected override void Update()
    {
        base.Update();
        
        // Existing mouse input logic...
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            if (swordType == SwordType.None)
                return;       
            finalDir = new Vector2(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y);
        }
       

        if (Input.mouseScrollDelta.y > 0.2f)
            NextSwordType();

        if (Input.mouseScrollDelta.y < -0.2f)
            PreviousSwordType();

        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (swordType == SwordType.None)
                return;

            for (int i = 0; i < dots.Length; i++)
                dots[i].transform.position = DotsPosition(i * spaceBetweenDots);
        }
    }

    // Create Sword
    public void CreateSword()
    {
        if (player.sword != null) return; // ? Player already has a sword, don't throw again

        GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
        SwordSkillController newSwordController = newSword.GetComponent<SwordSkillController>();

        if (swordType == SwordType.Bounce)
            newSwordController.SetupBounce(true, bounceAmount);
        else if (swordType == SwordType.Pierce)
            newSwordController.SetupPierce(pierceAmount);
        else if (swordType == SwordType.Spin)
            newSwordController.SetupSpin(true, maxTravelDistance, spinDuration, damageCooldown);

        newSwordController.SetUpSword(finalDir, swordGravity, player, freezeDur, returnSpeed);

        player.AssignNewSword(newSword);

        ActivateDots(false);
    }

    // Aim Sword
    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - playerPosition;

        return direction;
    }

    public void ActivateDots(bool _isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }

    private void GenerateDots()
    {
        dots = new GameObject[numberOfDots];

        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotParent);
            dots[i].SetActive(false);
        }
    }

    private Vector2 DotsPosition(float t)
    {
        Vector2 position;

        position = (Vector2)player.transform.position + new Vector2(AimDirection().normalized.x * launchForce.x,
                AimDirection().normalized.y * launchForce.y) * t + 0.5f * (Physics2D.gravity * swordGravity) * (t * t);

        return position;
    }

    //Check Unlockes
    public void CheckRegular()
    {
        if (regularSkillButton.unlocked)
        {
            isRegularUnlocked = true;

            if (unlockedTypes < 1)
                unlockedTypes++;

            inGameUI.SwitchSwordIcon(typeCounter);
            typeCounter = 1; // Set to Regular type if unlocked
            UpdateSwordType();
        }
    }


    public void CheckPierce()
    {
        if (pierceSkillButton.unlocked)
        {
            isPierceUnlocked = true;

            if (unlockedTypes < 2)
                unlockedTypes++;

           
        }
    }

    public void CheckSpin()
    {
        if (spinSkillButton.unlocked)
        {
            isSpinUnlocked = true;

            if (unlockedTypes < 3)
                unlockedTypes++;

           
        }
    }

    public void CheckBounce()
    {
        if (bounceSkillButton.unlocked)
        {
            isBounceUnlocked = true;

            if (unlockedTypes < 4)
                unlockedTypes++;

        }
    }

    public void NextSwordType()
    {
        typeCounter++;
        if (typeCounter > unlockedTypes && unlockedTypes > 0)
            typeCounter = 1;
        else if (typeCounter > unlockedTypes && unlockedTypes <= 0)
            typeCounter = 0;

        UpdateSwordType();
    }

    public void PreviousSwordType()
    {
        typeCounter--;
        if (typeCounter < 1 && unlockedTypes > 0)
            typeCounter = unlockedTypes;
        else if (typeCounter < 1 && unlockedTypes <= 0)
            typeCounter = 0;

        UpdateSwordType();
    }

    private void UpdateSwordType()
    {
        if (typeCounter == 1)
            swordType = SwordType.Regular;
        else if (typeCounter == 2)
            swordType = SwordType.Pierce;
        else if (typeCounter == 3)
            swordType = SwordType.Spin;
        else if (typeCounter == 4)
            swordType = SwordType.Bounce;
        else if (typeCounter < 1 || typeCounter > unlockedTypes)
            swordType = SwordType.None;

        SetupGravity();
        inGameUI.SwitchSwordIcon(typeCounter);
    }

    public void LoadData(GameData _data)
    {
        if(_data.typeCounter != 0)
            typeCounter = _data.typeCounter;
    }

    public void SaveData(ref GameData _data)
    {
        _data.typeCounter = typeCounter;
    }
}
