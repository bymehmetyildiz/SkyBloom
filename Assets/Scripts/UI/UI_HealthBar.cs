using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    private Entity entity;
    private RectTransform rectTransform;
    private Slider slider;
    private EntityStats entityStats;

    void Start()
    {
        slider = GetComponentInChildren<Slider>();
        rectTransform = GetComponent<RectTransform>();
        entity = GetComponentInParent<Entity>();
        entityStats = GetComponentInParent<EntityStats>();

        entity.onFlipped += FlipUI;
        entityStats.onHealthChanged += UpdateHealth;
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        slider.maxValue = entityStats.GetMaxHealth();
        slider.value = entityStats.currentHealth;
    }


    private void FlipUI() => rectTransform.Rotate(0, 180, 0);

    private void OnDisable()
    {
        entity.onFlipped -= FlipUI;
        entityStats.onHealthChanged -= UpdateHealth;
    }


}
