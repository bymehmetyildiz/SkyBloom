using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text characterName;
    public TMP_Text messageText;
    public GameObject backgroundBox;

    Message[] currentMessages;
    Character[] currentCharacters;
    int activeMessage = 0;

    public bool isActive = false;

    // Add a typing speed variable.
    public float typingSpeed = 30.0f;

    public bool isTyping = false;

    private void Start()
    {
        backgroundBox.SetActive(false);
    }

    public void OpenDialoue(Message[] messages, Character[] characters)
    {
        currentMessages = messages;
        currentCharacters = characters;
        activeMessage = 0;
        isActive = true;
        backgroundBox.SetActive(true);
        DisplayMessage();
    }

    private void DisplayMessage()
    {
        characterName.text = currentCharacters[currentMessages[activeMessage].characterID].name;
        StartCoroutine(TypeMessage(currentMessages[activeMessage].message));
        AudioManager.instance.PlaySFX(60, null);
    }

    // Coroutine to type the message letter by letter.
    private IEnumerator TypeMessage(string message)
    {
        isTyping = true;
        messageText.text = "";

       

        foreach (char letter in message)
        {
            messageText.text += letter;
            yield return new WaitForSeconds(1 / typingSpeed);

            // If E is pressed while typing, show full message instantly
            if (Input.GetKeyDown(KeyCode.E))
            {
                messageText.text = message;
                break; // Break out of the loop, but still stop the sound properly
            }
        }

        

        isTyping = false;
    }


    public void NextMessage()
    {
        if (isTyping)
        {
            // If typing, skip to the end of the current message
            StopAllCoroutines();
            messageText.text = currentMessages[activeMessage].message;
            isTyping = false;
            // Stop the typing sound
            
        }
        else
        { 
            
            // If not typing, move to the next message
            activeMessage++;

            if (activeMessage < currentMessages.Length)
            {
                DisplayMessage();
            }
            else
            {
                isActive = false;
                backgroundBox.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isActive)
        {
            NextMessage(); // Works both during typing and after
        }

        if(!isTyping)
            AudioManager.instance.StopSFX(60);
        else
            AudioManager.instance.PlaySFX(60, null);
    }
}
