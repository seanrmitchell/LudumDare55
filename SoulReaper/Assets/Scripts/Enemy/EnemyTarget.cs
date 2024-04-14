using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    public GameObject target;
    
    private GameObject playerTarget;

    private void Awake()
    {
        playerTarget = GameObject.Find("Player");
        target = playerTarget;
    }
    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Ally Creature"))
        {
            target = GameObject.FindGameObjectWithTag("Ally Creature");
        }
        else
        {
            target = playerTarget;
        }
    }
}

