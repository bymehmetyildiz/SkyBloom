using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckSword : MonoBehaviour
{
    
    void Start()
    {
        int stuck = Random.Range(0, 2);
        GetComponentInChildren<Animator>().SetInteger("Stuck", stuck);
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
