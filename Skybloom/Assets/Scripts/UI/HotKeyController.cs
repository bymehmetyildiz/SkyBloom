using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HotKeyController : MonoBehaviour
{
    private SpriteRenderer sr;
    private KeyCode hotKey;
    private TextMeshProUGUI hotKeyText;

    private Transform enemy;
    private BlackHoleSkillController blackHoleSkillController;

    void Start()
    {
        
    }

    public void SetupHotKey(KeyCode _hotKey, Transform _enemy, BlackHoleSkillController _blackHoleSkillController)
    {
        sr = GetComponent<SpriteRenderer>();
        hotKeyText = GetComponentInChildren<TextMeshProUGUI>();

        enemy = _enemy;
        blackHoleSkillController = _blackHoleSkillController;

        hotKey = _hotKey;
    }


    void Update()
    {
        //if (Input.GetKeyUp(hotKey))
        //{
        //    blackHoleSkillController.AddEnemyToList(enemy);

        //    hotKeyText.color = Color.clear;
        //    sr.color = Color.clear;
        //}
    }
}
