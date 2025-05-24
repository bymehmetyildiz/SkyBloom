using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_SkillTreeSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISaveManager
{
    private UI_Controller ui;

    [SerializeField] private int skillCost;
    [SerializeField] private string skillName;
    [TextArea]
    [SerializeField] private string skillDecription;
    

    public bool unlocked;

    [SerializeField] private UI_SkillTreeSlot[] shouldBeUnlocked;
    [SerializeField] private UI_SkillTreeSlot[] shouldBeLocked;

    [SerializeField] private Image skillImage;
    private UI_SkillLock skillLock;

    private void Awake()
    {
        GetComponentInChildren<Button>().onClick.AddListener(() => UnlockSkillSlot());
        
    }

    private void OnValidate()
    {
        gameObject.name = skillName;
    }

    void Start()
    {
        skillLock = GetComponentInChildren<UI_SkillLock>();
        skillImage = skillLock.GetComponent<Image>();
        skillImage.gameObject.SetActive(!unlocked);

        ui = GetComponentInParent<UI_Controller>();
        CheckThrowingSwordUnlock();

     
    }

    
    public void UnlockSkillSlot()
    {
        if (unlocked)
            return;

        if (PlayerManager.instance.HasEnoughMoney(skillCost) == false)
            return;

        for (int i = 0; i < shouldBeUnlocked.Length; i++)
        {
            if (shouldBeUnlocked[i].unlocked == false)
            {                 
                StartCoroutine(ui.ScalePanel("Unlock Previous Skill First!"));                
                return;
            }
        }

        PlayerManager.instance.SpendCurrency(skillCost);

        unlocked = true;
        AudioManager.instance.PlaySFX(61, null);
        CheckThrowingSwordUnlock();
        skillLock.UnlockSkill();
    }

   

    private static void CheckThrowingSwordUnlock()
    {
        PlayerManager.instance.player.skillManager.swordSkill.CheckRegular(); // Addlistener kýsmý iþe yaramýyordu burda. Bu yüzden bu þekilde yaptým.
        PlayerManager.instance.player.skillManager.swordSkill.CheckPierce();// Addlistener kýsmý iþe yaramýyordu burda. Bu yüzden bu þekilde yaptým.
        PlayerManager.instance.player.skillManager.swordSkill.CheckSpin();// Addlistener kýsmý iþe yaramýyordu burda. Bu yüzden bu þekilde yaptým.
        PlayerManager.instance.player.skillManager.swordSkill.CheckBounce();// Addlistener kýsmý iþe yaramýyordu burda. Bu yüzden bu þekilde yaptým.
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(skillDecription, skillName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillToolTip.HideToolTip();
    }

    public void LoadData(GameData _data)
    {
        if(_data.skillTree.TryGetValue(skillName, out bool value))
        {
            unlocked = value;
        }
    }

    public void SaveData(ref GameData _data)
    {
        if (_data.skillTree.TryGetValue(skillName, out bool value))
        {
            _data.skillTree.Remove(skillName);
            _data.skillTree.Add(skillName, unlocked);
        }
        else
            _data.skillTree.Add(skillName, unlocked);

    }
}
