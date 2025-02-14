using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class UI_InGame : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider healthBar;

    [Header("Throwing Sword")]
    [SerializeField] private Sprite[] swordSkillSprites;
    [SerializeField] private Image swordSkillImage;

    [SerializeField] private TextMeshProUGUI currencyText;



    void Start()
    {
        if(playerStats != null)        
            playerStats.onHealthChanged += UpdateHealth;
    }

    
    void Update()
    {
       currencyText.text = PlayerManager.instance.GetCurrency().ToString();
    }

    private void UpdateHealth()
    {
        healthBar.maxValue = playerStats.GetMaxHealth();
        healthBar.value = playerStats.currentHealth;
    }

    //private void SetCoolDown(Image _image)
    //{
    //    if(_image.fillAmount <= 0)
    //        _image.fillAmount = 1;
    //}

    //private void CheckCoolDown(Image _image, float _coolDown)
    //{
    //    if(_image.fillAmount > 0)
    //        _image.fillAmount -= 1/_coolDown * Time.deltaTime;
    //}

    //Throwing Sword Icons
    public void SwitchSwordIcon(int _index)
    {
        if (_index == 0)
            swordSkillImage.gameObject.SetActive(false);
        else if (_index > 0)
            swordSkillImage.gameObject.SetActive(true);


        swordSkillImage.sprite = swordSkillSprites[_index];
    }

}
