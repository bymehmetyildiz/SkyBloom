using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using CrazyGames;

public class GameManager : MonoBehaviour, ISaveManager
{
    public static GameManager instance;
    public bool isBossDead;
    public bool isPlayerEnetered;

    [Header("CheckPoints")]
    private CheckPoint[] checkPoints;
    private string closestCheckPointId;

    [Header("NPC")]
    private DialogueTrigger[] dialogueTriggers;

    [Header("Chest")]
    private Chest[] chests;

    [Header("Common")]
    [SerializeField] private EnemyStats boss;   
    [SerializeField] private DoorController enteranceGate;
    public DoorController exitGate;

    [Header("SilverStream")]
    [SerializeField] private GameObject ladder;

    private bool happyTirggered;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(instance.gameObject);

        CrazySDK.Init(() => { });

        checkPoints = FindObjectsOfType<CheckPoint>();

        dialogueTriggers = FindObjectsOfType<DialogueTrigger>();

        chests = FindObjectsOfType<Chest>();
    }

    private void Start()
    {
        isPlayerEnetered = false;
        happyTirggered = false;
    }

    private void Update()
    {
        if(boss !=null)
            if (boss.isDead)
            {
                IsBossDead();
                
                if (!happyTirggered)
                {
                    CrazySDK.Game.HappyTime();
                    happyTirggered=true;
                }

            }
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

            if (ladder != null)
                StartCoroutine(DestructLadder());
        }
    }

    private IEnumerator DestructLadder()
    {
        ladder.GetComponent<BoxCollider2D>().isTrigger = true;
        HingeJoint2D[] hinges = ladder.GetComponentsInChildren<HingeJoint2D>();

        foreach (var hinge in hinges)
        {
            hinge.enabled = false;
        }
        yield return new WaitForSeconds(2);
        Destroy(ladder);
    }


    public void IsBossDead()
    {
        if (exitGate == null)
            return;

        if (exitGate.isOpen)
            return;

        StartCoroutine(exitGate.OpenGate(0));
    }

    public void RestartScene()
    {
        SaveManager.instance.SaveGame();

        Scene scene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(scene.name);

        AudioManager.instance.PlaySFX(58, null);
    }

    public void ReturnToMenu()
    {
        SaveManager.instance.SaveGame();
        SceneManager.LoadScene("Menu");
        AudioManager.instance.PlaySFX(58, null);
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

        foreach (KeyValuePair<string, bool> _npc in _data.npc)
        {
            foreach (var npc in dialogueTriggers)
            {
                if(npc.name == _npc.Key && npc.name != "SuccubusNPC")
                    npc.isSpoken = _npc.Value;
            }
        }

        foreach (KeyValuePair<string, bool> _chest in _data.chest)
        {
            foreach (var chest in chests)
            {
                if(chest.id == _chest.Key)
                    chest.IsOpened = _chest.Value;
            }

        }


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
        var closest = FindClosestCheckPoint();
        _data.closestCheckPointId = closest != null ? closest.id : string.Empty;

        _data.checkPoints.Clear();

        foreach (CheckPoint checkPoint in checkPoints)
        {
            _data.checkPoints.Add(checkPoint.id, checkPoint.isActivated);
        }

        _data.npc.Clear();
        foreach (var npc in dialogueTriggers)
        {
            _data.npc.Add(npc.name, npc.isSpoken);
        }

        _data.chest.Clear();

        foreach (var chest in chests)
        {
            _data.chest.Add(chest.id, chest.IsOpened);
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

    public void PauseGame(bool _isPaused)
    {
        if (_isPaused)
        {        
            Time.timeScale = 0.0f;
            CrazySDK.Game.GameplayStop();
        }
        else
        {          
            Time.timeScale = 1.0f;
            CrazySDK.Game.GameplayStart();
        }

    }

    private void OnApplicationQuit()
    {
        SaveManager.instance.SaveGame();
    }
}
