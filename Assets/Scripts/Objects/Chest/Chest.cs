using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.LucidEditor;


public class Chest : MonoBehaviour
{
    [FoldoutGroup("Reference")]
    public Animator animator;
    public GameObject key;
    public GameObject button;
    private bool isPlayerNearby;
    private ItemDrop itemDrop;
    public string id;


    private void Start()
    {
        key.SetActive(false);
        button.SetActive(false);
        itemDrop = GetComponent<ItemDrop>();

        Invoke("CheckOpenedChest", 0.1f);
         
    }

    [ContextMenu("Generate Checkpoint Id")]
    private void GenerateId()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void CheckOpenedChest()
    {
        if (isOpened)
            Open();
    }

    [FoldoutGroup("Runtime"), ShowInInspector, DisableInEditMode]
    public bool IsOpened
    {
        get { return isOpened; }
        set
        {
            isOpened = value;
            animator.SetBool("IsOpened", isOpened);
        }
    }
    private bool isOpened;

    [FoldoutGroup("Runtime"),Button("Open"), HorizontalGroup("Runtime/Button")]
    public void Open()
    {
        IsOpened = true;
    }

    [FoldoutGroup("Runtime"), Button("Close"), HorizontalGroup("Runtime/Button")]
    public void Close()
    {
        IsOpened = false;
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (!IsOpened)
            {                
                OpenChest();
            }

        }
    }

    public void OpenChest()
    {
        Open();
        AudioManager.instance.PlaySFX(12, this.transform);
        key.SetActive(false);
        button.SetActive(false);
        itemDrop.GenerateDrop();
        SaveManager.instance.SaveGame();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            isPlayerNearby = true;

            if (PlatformUtils.IsWebGLMobile())
            {
                if (!isOpened)
                    button.SetActive(true);
            }
            else
            {
                if (!isOpened)
                    key.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            isPlayerNearby = false;
            key.SetActive(false);
            button.SetActive(false);
        }
    }
}

