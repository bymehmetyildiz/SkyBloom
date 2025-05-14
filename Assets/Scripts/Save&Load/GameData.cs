using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int currency; // Money
    public int typeCounter; // Sword Throw Skill Type Counter
    public int sceneIndex; // Scene Index
    public bool isOpened; // Is Chest Opeened
   

    public SerializableDictionary<string, bool> skillTree;
    public SerializableDictionary<string, int> inventory;
    public SerializableDictionary<string, int> equipment;

    public SerializableDictionary<string, bool> checkPoints;
    public string closestCheckPointId;

    public SerializableDictionary<string, bool> npc;

    public SerializableDictionary<string, bool> chest;

    public GameData()
    {
        currency = 0;        
        typeCounter = 0;
        sceneIndex = 1;
        isOpened = false;
       
        
        skillTree = new SerializableDictionary<string, bool>();
        inventory = new SerializableDictionary<string, int>();
        equipment = new SerializableDictionary<string, int>();

        checkPoints = new SerializableDictionary<string, bool>();
        closestCheckPointId = string.Empty;

        npc = new SerializableDictionary<string, bool>();

        chest = new SerializableDictionary<string, bool>();
    }

}
