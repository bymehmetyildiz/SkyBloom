using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SignBoardController : MonoBehaviour
{
    [SerializeField] private GameObject tipPanel;
    [SerializeField] private GameObject interactKey;
    [SerializeField] private GameObject interactButton;
    [SerializeField] private TMP_Text header;
    [SerializeField] private TMP_Text description;
    [TextArea(3, 10)]
    [SerializeField] private string descriptionTextPC;
    [TextArea(3, 10)]
    [SerializeField] private string descriptionTextMobile;
    [SerializeField] private string headerText;
    private bool canInteract;

    private void Start()
    {
        tipPanel.SetActive(false);
        interactKey.SetActive(false);
        interactButton.SetActive(false);
        canInteract = false;
    }

    private void Update()
    {
        if(canInteract)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }

        }
    }

    public void Interact()
    {
        AudioManager.instance.PlaySFX(59, null);

        if (interactKey.activeSelf == true)
            interactKey.SetActive(false);

        if(PlatformUtils.IsWebGLMobile())
            if (interactButton.activeSelf == true)
                interactButton.SetActive(false);


        if (tipPanel.activeSelf == false)
        {
            tipPanel.SetActive(true);
            header.text = headerText;
            if (PlatformUtils.IsWebGLMobile())
                description.text = descriptionTextMobile;
            else
                description.text = descriptionTextPC;
        }
        else if (tipPanel.activeSelf == true)
            tipPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            if(PlatformUtils.IsWebGLMobile())
            {
                if (interactButton.activeSelf == false)
                    interactButton.SetActive(true);
            }
            else
            {
                if (interactKey.activeSelf == false)
                    interactKey.SetActive(true);
            }

            canInteract = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (interactKey.activeSelf == true)
                interactKey.SetActive(false);

            else if (tipPanel.activeSelf == true)
                tipPanel.SetActive(false);

            if (interactButton.activeSelf == true)
                interactButton.SetActive(false);
            canInteract = false;
        }
    }


}
