using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSkillController : MonoBehaviour
{
    private Player player;
    [SerializeField] private GameObject thunder;
    private float maxSize;
    private float growSpeed;
    public bool canGrow;    
    private float offset;
    private int damage;
    private float speed;

    public List<Transform> targets = new List<Transform>();

    void Start()
    {
       
    }

    public void SetUpThunderSkill(int _damage, float _maxSize, float _growSpeed, float _speed, float _offset, Player _player)
    {      
        damage = _damage;  
        growSpeed = _growSpeed;
        maxSize = _maxSize;
        speed = _speed;
        offset = _offset;
        player = _player;
    }

    
    void Update()
    {
        if (canGrow)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);

        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector2(0, 0), growSpeed * Time.deltaTime);
        }
    }



    // StrikeThunder After BlackHole
    public IEnumerator StrikeThunder(float _seconds)
    {
        yield return new WaitForSeconds(_seconds);

        foreach (Transform t in new List<Transform>(targets))
        {
            GameObject newThunder = Instantiate(thunder, new Vector2(t.position.x, t.position.y + offset), Quaternion.identity);
            newThunder.GetComponent<Thunder>().SetUpThunder(speed, damage, player);
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(_seconds);

        canGrow = false;

        yield return new WaitForSeconds(_seconds);
            
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FreezeTime(true);
            AddEnemyToList(collision.transform);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FreezeTime(false);
            RemoveEnemyFromList(collision.transform);
        }
    }

    public void AddEnemyToList(Transform _enemy) => targets.Add(_enemy);

    public void RemoveEnemyFromList(Transform _enemy) => targets.Remove(_enemy);
}
