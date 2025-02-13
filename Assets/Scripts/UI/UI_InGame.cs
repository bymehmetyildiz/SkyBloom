using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class UI_InGame : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] Slider healthBar;
    
    void Start()
    {
        if(playerStats != null)
        {
            playerStats.onHealthChanged += UpdateHealth;
        }
    }

    
    void Update()
    {
    }

    private void UpdateHealth()
    {
        healthBar.maxValue = playerStats.GetMaxHealth();
        healthBar.value = playerStats.currentHealth;
    }
}
