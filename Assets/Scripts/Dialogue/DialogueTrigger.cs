using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueTrigger : MonoBehaviour
{    
    public Message[] messages;
    public Character[] characters;   
    private bool isPlayed;

    [SerializeField] private DialogueManager dialogueManager;
    public bool isSpoken = false;
    [SerializeField] private CapsuleCollider2D cc;

    private bool canSpeak;
    [SerializeField] private GameObject interactKey;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>(true);
        cc = GetComponent<CapsuleCollider2D>();
        canSpeak = false;
        interactKey.SetActive(false);
        isPlayed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (interactKey.activeSelf == false && !isSpoken)
                interactKey.SetActive(true);

            canSpeak = true;

            if (isPlayed == false)
            {
                if (this.gameObject.name == "SuccubusNPC")
                {
                    AudioManager.instance.PlaySFX(54, null);
                    isPlayed = true;
                }
            }

           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (interactKey.activeSelf == true)
                interactKey.SetActive(false);

            canSpeak = false;

            if (dialogueManager != null)
            {
                dialogueManager.isActive = false;
                dialogueManager.backgroundBox.SetActive(false);
            }
        }
    }
   

    private void Update()
    {
        if (canSpeak)
        {
            if (Input.GetKeyDown(KeyCode.E) && !isSpoken)
            {
                dialogueManager.OpenDialoue(messages, characters);
                isSpoken = true;
                AudioManager.instance.PlaySFX(59, null);

                if (interactKey.activeSelf == true)
                    interactKey.SetActive(false);
            }
        }

        if (isSpoken && !dialogueManager.isActive)
        {
            if(cc != null)
                cc.isTrigger = true;
        }
    }
}


[System.Serializable]
public class Message
{
    public int characterID;
    [TextArea(3, 10)]
    public string message;
}

[System.Serializable]
public class Character
{
    public string name;
}