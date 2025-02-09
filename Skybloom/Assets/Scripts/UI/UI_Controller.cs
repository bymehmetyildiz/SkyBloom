using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    public static UI_Controller instance;

    [SerializeField] private GameObject[] inventoryElemnets;

    [Header("Throwing Sword")]
    [SerializeField] private Sprite[] swordSkillSprites;
    [SerializeField] private Image swordSkillImage;



    public UI_ItemToolTip itemToolTip;
    public UI_StatToolTip statToolTip;
    public UI_SkillToolTip skillToolTip;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance.gameObject);
    }

    private void Start()
    {
        Switch(null);

        itemToolTip.gameObject.SetActive(false);
        statToolTip.gameObject.SetActive(false);
        skillToolTip.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
            SwitchWithKey(inventoryElemnets[0]);
    }

    public void Switch(GameObject _menu)
    {
        for (int i = 0; i < inventoryElemnets.Length; i++)
        {
            inventoryElemnets[i].gameObject.SetActive(false);
        }

        if (_menu != null)
        {
            _menu.SetActive(true);
        }
       
    }

    
    public void SwitchWithKey(GameObject _menu)
    {
        if (_menu != null && _menu.activeSelf)
        {
            _menu.SetActive(false);
            return;
        }
        Switch(_menu);

    }

    //Throwing Sword Icons
    public void SwitchIcon(int _index)
    {
        if(_index == 0)
            swordSkillImage.gameObject.SetActive(false);
        else if (_index > 0)
            swordSkillImage.gameObject.SetActive(true);


        swordSkillImage.sprite = swordSkillSprites[_index];
    }


}
