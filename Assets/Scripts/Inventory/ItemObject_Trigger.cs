using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject_Trigger : MonoBehaviour
{
    private ItemObject itemObject => GetComponentInParent<ItemObject>();
    [SerializeField] private float groundCheckDist;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if(collision.GetComponent<PlayerStats>().isDead)
                return;

            if(IsGroundDetected())
                itemObject.PickUpItem();
        }
    }


    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDist, groundMask);

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDist));
    }
}
