using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    private Entity entity => GetComponentInParent<Entity>();
    private EntityStats entityStats => GetComponentInParent<EntityStats>();
    private RectTransform rectTransform;
    private Slider slider;

    void Start()
    {
        slider = GetComponentInChildren<Slider>();
        rectTransform = GetComponent<RectTransform>();
        
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        slider.maxValue = entityStats.GetMaxHealth();
        slider.value = entityStats.currentHealth;
    }


    private void FlipUI() => rectTransform.Rotate(0, 180, 0);

    private void OnEnable()
    {
        entity.onFlipped += FlipUI;
        entityStats.onHealthChanged += UpdateHealth;
    }

    private void OnDisable()
    {
        if(entity != null)
            entity.onFlipped -= FlipUI;

        if(entityStats != null)
            entityStats.onHealthChanged -= UpdateHealth;
    }


}
