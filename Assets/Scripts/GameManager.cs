using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isBossDead;
    public bool isPlayerEnetered;

    [Header("Common")]
    [SerializeField] private EnemyStats boss;

    [Header("Level 1")]
    [SerializeField] private DoorController enteranceGate;
    public DoorController exitGate;
 

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(instance.gameObject);
    }

    private void Start()
    {
        isPlayerEnetered = false;
    }

    private void Update()
    {
        if(boss !=null)
            if (boss.isDead)
                IsBossDead();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            isPlayerEnetered = true;
            if(enteranceGate != null)
            {
                if (enteranceGate.isOpen)
                    StartCoroutine(enteranceGate.CloseGate(0));
            }

        }
    }

    public void IsBossDead()
    {
        if (exitGate.isOpen)
            return;

        if(exitGate.isOpen == false)
            StartCoroutine(exitGate.OpenGate(0));
    }

}
