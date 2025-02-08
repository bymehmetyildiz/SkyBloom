using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DropSwordContoller : MonoBehaviour
{
    private Player player;

    private float speed;
    private float destroyDur;
    SwordRainSkill swordRainSkillController;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    
    

    void Start()
    {
        if (IsGroundDetected())
            Destroy(gameObject);
        else
            Destroy(gameObject, destroyDur);  
        
        player = FindObjectOfType<Player>();
    }

    public void SetUpRainSword(float _speed, SwordRainSkill _swordRainSkillController, float _destroyDur)
    {
        speed = _speed;
        swordRainSkillController = _swordRainSkillController;
        destroyDur = _destroyDur;
    }

    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed);
        if (IsGroundDetected())
        {
            swordRainSkillController.GroundStuckSword(groundCheck.position);
            swordRainSkillController.SpawnHitEffect(new Vector2(groundCheck.position.x , groundCheck.position.y));
            Destroy(gameObject);
          
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            player.stats.DoDamage(collision.GetComponent<EnemyStats>(), 0);
            Destroy(gameObject);
        }
    }

    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));        
    }

}
