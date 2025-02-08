using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] private GameObject equipmentUI;
    //[SerializeField] private GameObject skillTreeUI;
    //[SerializeField] private GameObject craftUI;
    //[SerializeField] private GameObject settingsUI;

    public UI_ItemToolTip itemToolTip;
    public UI_StatToolTip statToolTip;
    public UI_SkillToolTip skillToolTip;

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
            SwitchWithKey(equipmentUI);
    }

    public void Switch(GameObject _menu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if (_menu != null)
        {
            _menu.SetActive(true);
            
        }
       
    }

    
    public void SwitchWithKey(GameObject _menu)
    {
        if (_menu == null && _menu.activeSelf)
        {
            _menu.SetActive(false);
            return;
        }

        Switch(_menu);

    }

}
