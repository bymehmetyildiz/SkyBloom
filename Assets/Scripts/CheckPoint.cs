using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour, ISaveManager
{
    [SerializeField] private Animator anim;
    public string id;
    public bool isActivated;

    void Awake()
    {
        anim = GetComponent<Animator>();     
    }

    [ContextMenu("Generate Checkpoint Id")]
    private void GenerateId()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            ActivateCheckPoint();
        }
    }

    public void ActivateCheckPoint()
    {
        isActivated = true;
        anim.SetBool("Lit", isActivated);
    }

    public void LoadData(GameData _data)
    {
        if (_data != null)
            this.isActivated = _data.isActivated;
        else
            this.isActivated = false;
    }

    public void SaveData(ref GameData _data)
    {
        _data.isActivated = isActivated;
    }
}
