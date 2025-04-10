using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private GameObject fire;
    public string id;
    public bool isActivated;

    void Start()
    {
     
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
        fire.SetActive(true);
    }



}
