using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveManager
{
    public static GameManager instance;
    public bool isBossDead;
    public bool isPlayerEnetered;

    [Header("CheckPoints")]
    private CheckPoint[] checkPoints;
    private string closestCheckPointId;
    

    [Header("Common")]
    [SerializeField] private EnemyStats boss;

    [Header("Level 1")]
    [SerializeField] private DoorController enteranceGate;
    public DoorController exitGate;
 

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(instance.gameObject);

        checkPoints = FindObjectsOfType<CheckPoint>();
    }

    private void Start()
    {
        isPlayerEnetered = false;
        
    }

    


    private void Update()
    {
        if(boss !=null)
            if (boss.isDead)
                IsBossDead();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            isPlayerEnetered = true;
            if(enteranceGate != null)
            {
                if (enteranceGate.isOpen)
                    StartCoroutine(enteranceGate.CloseGate(0));
            }

        }
    }

    public void IsBossDead()
    {
        if (exitGate.isOpen)
            return;

        if(exitGate.isOpen == false)
            StartCoroutine(exitGate.OpenGate(0));
    }

    public void RestartScene()
    {
        SaveManager.instance.SaveGame();

        Scene scene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(scene.name);
    }

    public void LoadData(GameData _data)
    {
        foreach (KeyValuePair<string, bool> pair in _data.checkPoints)
        {

            foreach (CheckPoint checkPoint in checkPoints)
            {
                if (checkPoint.id == pair.Key && pair.Value == true)
                    checkPoint.ActivateCheckPoint();

            }
        }

        closestCheckPointId = _data.closestCheckPointId;
        //Invoke("PlacePlayerAtClosestCheckPoint", 0.1f);
        PlacePlayerAtClosestCheckPoint();
    }

    private void PlacePlayerAtClosestCheckPoint()
    {
        foreach (CheckPoint checkPoint in checkPoints)
        {
            if (closestCheckPointId == checkPoint.id)
                PlayerManager.instance.player.transform.position = checkPoint.transform.position;
        }
    }

    public void SaveData(ref GameData _data)
    {
        _data.closestCheckPointId = FindClosestCheckPoint().id;
        _data.checkPoints.Clear();

        foreach (CheckPoint checkPoint in checkPoints)
        {
            _data.checkPoints.Add(checkPoint.id, checkPoint.isActivated);
        }
    }

    private CheckPoint FindClosestCheckPoint()
    {
        float closestDistance = Mathf.Infinity;
        CheckPoint closestCheckPoint = null;

        foreach (var checkPoint in checkPoints)
        {
            float distanceTocheckPoint = Vector2.Distance(PlayerManager.instance.player.transform.position, checkPoint.transform.position);

            if (distanceTocheckPoint < closestDistance && checkPoint.isActivated == true)
            {
                closestDistance = distanceTocheckPoint;
                closestCheckPoint = checkPoint;
            }

        }

        return closestCheckPoint;
    }

}
