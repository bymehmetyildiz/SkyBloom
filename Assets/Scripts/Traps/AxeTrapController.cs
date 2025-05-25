using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeTrapController : MonoBehaviour
{
    private bool canMove;
    private bool isActivated;
    private bool isWarned;
    [SerializeField] private GameObject warning;
    [SerializeField] private int moveSpeed;
    [SerializeField] private AudioSource axeTrapSFX;

    void Start()
    {
        warning.SetActive(false);
    }


    void Update()
    {
        if (canMove)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.Euler(0, 0, 0),
                moveSpeed * Time.deltaTime
            );

            if (Mathf.Abs(transform.eulerAngles.z) < 2 || Mathf.Abs(transform.eulerAngles.z) > 358)
            {
                canMove = false;
                isActivated = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            StartCoroutine(ActivateWarning());
        }
    }

    private IEnumerator ActivateWarning()
    {
        if (!isActivated)
        {
            if(!isWarned)
                warning.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            isWarned = true;
            warning.SetActive(false);            
            canMove = true;
            axeTrapSFX.Play();
        }
    }

}
