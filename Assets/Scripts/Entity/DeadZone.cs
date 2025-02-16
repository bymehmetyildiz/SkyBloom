using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<EntityStats>() != null)
        {
            collision.GetComponent<EntityStats>().KillEntity();
        }
        else
            Destroy(collision.gameObject);
    }
}
