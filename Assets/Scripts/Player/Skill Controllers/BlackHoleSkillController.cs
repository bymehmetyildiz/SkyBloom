using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlackHoleSkillController : MonoBehaviour
{
    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private KeyCode hotKey;
    [SerializeField] private Player player;
    [SerializeField] private bool canSpawnHotKey;


    public float maxSize;
    public float growSpeed;
    public bool canGrow;

    public List<Transform> targets = new List<Transform>();

    void Start()
    {
        
    }


    void Update()
    {      

        if (canGrow)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
    
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FreezeTime(true);
            AddEnemyToList(collision.transform);
            
        }
    }

    private void SpawnHotKey(Collider2D collision)
    {
        GameObject newHotKey = Instantiate(hotKeyPrefab, player.transform.position + new Vector3(0, 1), Quaternion.identity);

        HotKeyController hotKeyController = newHotKey.GetComponent<HotKeyController>();

        hotKeyController.SetupHotKey(hotKey, collision.transform, this);
    }

    public void AddEnemyToList(Transform _enemy) => targets.Add(_enemy);
}
