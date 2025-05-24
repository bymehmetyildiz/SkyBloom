using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkillController : MonoBehaviour
{
    public Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cc;
    private Player player;

    [Header("Regular Info")]
    private bool canRotate = true;
    private bool isReturning;
    [SerializeField] private float returnRotSpeed;
    private float returnSpeed;
    private float freezeDur;

    [Header("Bounce Info")]
    private bool isBouncing;
    private int amountOfBounce;
    private int targetIndex;
    [SerializeField] private float bounceSpeed;
    private List<Transform> enemies;

    [Header("Pierce Info")]
    private int pierceAmount;

    [Header("Spin Info")]
    private float maxTravelDis;
    private float spinDur;
    private float spinTimer;
    private bool isStopped;
    private bool isSpinnig;
    private float damageTimer;
    private float damageCooldown;
    private float spinDir;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        StartCoroutine(DestroySword());
    }

    private IEnumerator DestroySword()
    {
        yield return new WaitForSeconds(7.0f);
        Destroy(this.gameObject);
        AudioManager.instance.StopSFX(18);
    }

    public void SetUpSword(Vector2 _dir, float _gravityScale, Player _player, float _freezeDur, float _returnSpeed)
    {
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;
        player = _player;
        freezeDur = _freezeDur;
        returnSpeed = _returnSpeed;

        spinDir = Mathf.Clamp(rb.velocity.x, -1, 1);
    }


    // Sword Type Setup
    public void SetupBounce(bool _isBouncing, int _amountOfBounce)
    {
        isBouncing = _isBouncing;
        amountOfBounce = _amountOfBounce;
        enemies = new List<Transform>();
    }

    public void SetupPierce(int _pierceAmount)
    {
        pierceAmount = _pierceAmount;
    }

    public void SetupSpin(bool _isSpinning, float _maxTravelDistance, float _spinDuration, float _damageCooldown)
    {
        isSpinnig = _isSpinning;
        maxTravelDis = _maxTravelDistance;
        spinDur = _spinDuration;
        damageCooldown = _damageCooldown;
    }


    public void ReturnSword()
    {
        rb.isKinematic = true;
        rb.gravityScale = 0.0f;
        rb.velocity = Vector2.zero;
        transform.parent = null;
        isReturning = true;
        anim.SetBool("Flip", false);
        AudioManager.instance.StopSFX(18);
    }

    


    private void Update()
    {
        if (canRotate && !isReturning)
            transform.right = rb.velocity;

        if (isReturning)
        {            
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);
            transform.Rotate(0, 0, returnRotSpeed);
            AudioManager.instance.PlaySFX(18, null);
            if (Vector2.Distance(transform.position, player.transform.position) < 1)
                player.CatchSword();
        }

        Bounce();
        Spin();
    }

    private void Spin()
    {
        if (isSpinnig)
        {
            AudioManager.instance.PlaySFX(18, null);
            if (Vector2.Distance(player.transform.position, transform.position) > maxTravelDis && !isStopped)
            {
                StopForSpinnig();

            }
            if (isStopped)
            {
                spinTimer -= Time.deltaTime;

                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + spinDir, transform.position.y), 1.5f * Time.deltaTime);

                if (spinTimer < 0)
                {
                    isReturning = true;
                    isSpinnig = false;
                    anim.SetBool("Flip", false);
                }

                damageTimer -= Time.deltaTime;

                if (damageTimer < 0)
                {
                    damageTimer = damageCooldown;

                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);

                    foreach (var hit in colliders)
                    {
                        if (hit.GetComponent<Enemy>() != null)
                            player.stats.DoDamage(hit.GetComponent<EnemyStats>());
                    }
                }
            }
        }
    }

    private void StopForSpinnig()
    {
        isStopped = true;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        spinTimer = spinDur;
        anim.SetBool("Flip", true);
    }

    private void Bounce()
    {
        if (isBouncing && enemies.Count > 0)
        {
            AudioManager.instance.PlaySFX(18, null);
            // Clean up null entries (e.g., destroyed enemies)
            enemies.RemoveAll(e => e == null);

            // Exit early if no valid enemies remain
            if (enemies.Count == 0)
            {
                isBouncing = false;
                isReturning = true;
                return;
            }

            Transform currentTarget = enemies[targetIndex];

            transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, bounceSpeed * Time.deltaTime);
            transform.Rotate(0, 0, returnRotSpeed);

            if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
            {
                var enemyStats = currentTarget.GetComponent<EnemyStats>();
                if (enemyStats != null)
                    player.stats.DoDamage(enemyStats);

                targetIndex++;
                amountOfBounce--;

                if (targetIndex >= enemies.Count)
                    targetIndex = 0;

                if (amountOfBounce <= 0)
                {
                    isBouncing = false;
                    isReturning = true;
                }
            }

            anim.SetBool("Flip", isBouncing);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReturning)
            return;

        if (collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            SwordSkillDamage(enemy);
        }

        BounceSword(collision);

        StuckSword(collision);
    }

    private void SwordSkillDamage(Enemy _enemy)
    {
        player.stats.DoDamage(_enemy.GetComponent<EnemyStats>());
        _enemy.GetComponent<EnemyStats>().isDamaged = true;
        _enemy.StartCoroutine(_enemy.FreezeTimeFor(freezeDur));

        ItemData_Equipment equipedAmulet = Inventory.instance.GetEquipment(EquipmentType.Amulet);

        if (equipedAmulet != null)
            equipedAmulet.Effect(_enemy.transform);
    }

    private void BounceSword(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            if (isBouncing && enemies.Count <= 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);

                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Enemy>() != null && collision.GetComponent<Enemy>().stats.isDead == false)
                        enemies.Add(hit.transform);
                }

            }

        }
    }

    private void StuckSword(Collider2D collision)
    {
        if(pierceAmount > 0 && collision.GetComponent<Enemy>() != null)
        {
            pierceAmount--;
            return;
        }

        if (isSpinnig)
        {
            StopForSpinnig();
            return;
        }



        canRotate = false;
        cc.enabled = false;

        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        if (isBouncing)
            return;

        transform.parent = collision.transform;

    }
}
