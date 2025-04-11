using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
                Debug.Log("Can not unlock skill");
                return;
            }
        }

        PlayerManager.instance.SpendCurrency(skillCost);

        unlocked = true;
        CheckThrowingSwordUnlock();
        skillLock.UnlockSkill();
    }

    private static void CheckThrowingSwordUnlock()
    {
        PlayerManager.instance.player.skillManager.swordSkill.CheckRegular(); // Addlistener k�sm� i�e yaram�yordu burda. Bu y�zden bu �ekilde yapt�m.
        PlayerManager.instance.player.skillManager.swordSkill.CheckPierce();// Addlistener k�sm� i�e yaram�yordu burda. Bu y�zden bu �ekilde yapt�m.
        PlayerManager.instance.player.skillManager.swordSkill.CheckSpin();// Addlistener k�sm� i�e yaram�yordu burda. Bu y�zden bu �ekilde yapt�m.
        PlayerManager.instance.player.skillManager.swordSkill.CheckBounce();// Addlistener k�sm� i�e yaram�yordu burda. Bu y�zden bu �ekilde yapt�m.
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
