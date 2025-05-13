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

    [Header("NPC")]
    private DialogueTrigger[] dialogueTriggers;
    

    [Header("Common")]
    [SerializeField] private EnemyStats boss;

    [Header("Level 1")]
    [SerializeField] private DoorController enteranceGate;
    public DoorController exitGate;

    [Header("The River")]
    [SerializeField] private GameObject ladder;

    

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(instance.gameObject);

        checkPoints = FindObjectsOfType<CheckPoint>();

        dialogueTriggers = FindObjectsOfType<DialogueTrigger>();
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

    public void ReturnToMenu()
    {
        SaveManager.instance.SaveGame();
        SceneManager.LoadScene("Menu");
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
        if(_isPaused)
            Time.timeScale = 0.0f;
        else
            Time.timeScale = 1.0f;
    }

    private void OnApplicationQuit()
    {
        SaveManager.instance.SaveGame();
    }
}
