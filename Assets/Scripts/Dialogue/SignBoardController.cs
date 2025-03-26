using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SignBoardController : MonoBehaviour
{
    [SerializeField] private GameObject tipPanel;
    [SerializeField] private GameObject interactKey;
    [SerializeField] private TMP_Text description;
    [TextArea(3, 10)]
    [SerializeField] private string descriptionText;
    private bool canInteract;

    private void Start()
    {
        tipPanel.SetActive(false);
        interactKey.SetActive(false);
        canInteract = false;
    }

    private void Update()
    {
        if(canInteract)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (interactKey.activeSelf == true)
                    interactKey.SetActive(false);
                else if (interactKey.activeSelf == false)
                    interactKey.SetActive(true);

                if (tipPanel.activeSelf == false)
                {
                    tipPanel.SetActive(true);
                    description.text = descriptionText;
                }
                else if (tipPanel.activeSelf == true)
                    tipPanel.SetActive(false);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            if (interactKey.activeSelf == false)
                interactKey.SetActive(true);

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

            canInteract = false;
        }
    }


}
