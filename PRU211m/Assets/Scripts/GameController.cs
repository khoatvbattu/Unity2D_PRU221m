﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject fillPrefab;     // Hold prefab object
    [SerializeField] Transform[] allCells;      // Array of transform

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnFill();
        }
    }

    public void SpawnFill()
    {
        float chance = Random.Range(0f, 1f);
        Debug.Log(chance);                      // Check chance

        // Chance that will not instantiate any object
        if (chance <.2f)        
        {
            return;
        }

        // Chance that will instantiate one fill object with the value 2
        else if (chance <.8f)                                                               
        {
            int WhichSpawn = Random.Range(0, allCells.Length);                              // Random which transform we want to instantiate a new fill object
            GameObject tempFill = Instantiate(fillPrefab, allCells[WhichSpawn]);            // Instantiate a new prefab
            Debug.Log(2);
        }

        // Chance that will instantiate one fill object with the value 4
        else
        {
            int WhichSpawn = Random.Range(0, allCells.Length);                              // Random which transform we want to instantiate a new fill object
            GameObject tempFill = Instantiate(fillPrefab, allCells[WhichSpawn]);            // Instantiate a new prefab
            Debug.Log(4);
        }
    }
}