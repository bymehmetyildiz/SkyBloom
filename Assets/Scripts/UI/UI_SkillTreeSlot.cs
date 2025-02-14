using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillTreeSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private UI_Controller ui;

    [SerializeField] private string skillName;
    [TextArea]
    [SerializeField] private string skillDecription;
    [SerializeField] private int skillPrice;

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

    }

    
    public void UnlockSkillSlot()
    {
        if (unlocked)
            return;

        if (PlayerManager.instance.HasEnoughMoney(skillPrice) == false)
            return;

        for (int i = 0; i < shouldBeUnlocked.Length; i++)
        {
            if (shouldBeUnlocked[i].unlocked == false)
            {
                Debug.Log("Can not unlock skill");                
                return;
            }
        }

        for (int i = 0; i < shouldBeLocked.Length; i++)
        {
            if (shouldBeLocked[i].unlocked == true)
            {
                Debug.Log("Can not unlock skill");                
                return;
            }
        }

        unlocked = true;        
        skillLock.UnlockSkill();
        
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(skillDecription, skillName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillToolTip.HideToolTip();
    }
}
