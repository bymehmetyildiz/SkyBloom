using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int currency;
    public int typeCounter;


    public SerializableDictionary<string, bool> skillTree;
    public SerializableDictionary<string, int> inventory;
    public SerializableDictionary<string, int> equipment;
    

    public GameData()
    {
        currency = 0;        
        typeCounter = 0;        
        skillTree = new SerializableDictionary<string, bool>();
        inventory = new SerializableDictionary<string, int>();
        equipment = new SerializableDictionary<string, int>();
    }

}
