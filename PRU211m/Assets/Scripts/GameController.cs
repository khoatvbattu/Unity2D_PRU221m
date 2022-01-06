using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public static int ticker;                   // Use to count how many cells have received our action

    [SerializeField] GameObject fillPrefab;     // Hold prefab object
    [SerializeField] Cell2048[] allCells;      // Array of transform

    public static Action<string> slide;
    public int myScore;
    [SerializeField] Text scoreDisplay;

    int isGameOver;
    [SerializeField] GameObject gameOverPanel;

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartSpawnFill();
        StartSpawnFill();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnFill();
        }

        // Input controls the update function
        if (Input.GetKeyDown(KeyCode.A))
        {
            ticker = 0;
            isGameOver = 0;

            // Broadcast a message through our action
            slide("a");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            ticker = 0;
            isGameOver = 0;

            // Broadcast a message through our action
            slide("d");

        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            ticker = 0;
            isGameOver = 0;

            // Broadcast a message through our action
            slide("w");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            ticker = 0;
            isGameOver = 0;

            // Broadcast a message through our action
            slide("s");
        }
    }

    public void SpawnFill()
    {
        bool isFull = true;
        
        // Loop through our all cells array and check to see if there's at least one empty cell
        for(int i = 0; i < allCells.Length; i++)
        {
            if(allCells[i].fill == null)
            {
                isFull = false;
            }
        }

        if (isFull == true)
        {
            return;
        }

        int WhichSpawn = UnityEngine.Random.Range(0, allCells.Length);                              // Random which transform we want to instantiate a new fill object

        // Check if the spawn point already has a child's object
        if(allCells[WhichSpawn].transform.childCount != 0)
        {
            Debug.Log(allCells[WhichSpawn].name + " is already filled");
            SpawnFill();
            return;
        }

        float chance = UnityEngine.Random.Range(0f, 1f);
        Debug.Log(chance);                      // Check chance

        // Chance that will not instantiate any object
        if (chance <.2f)        
        {
            return;
        }

        // Chance that will instantiate one fill object with the value 2
        else if (chance <.8f)                                                               
        {
            GameObject tempFill = Instantiate(fillPrefab, allCells[WhichSpawn].transform);            // Instantiate a new prefab
            Debug.Log(2);

            // Pass the value of the fill object into to the fill value update function of the newly instantiated fill prefab
            Fill2048 tempFillComp = tempFill.GetComponent<Fill2048>();

            // Get the cell script from the current cell that we are instantiating this fill prefab onto
            allCells[WhichSpawn].GetComponent<Cell2048>().fill = tempFillComp;

            tempFillComp.FillValueUpdate(2);
        }

        // Chance that will instantiate one fill object with the value 4
        else
        {
            GameObject tempFill = Instantiate(fillPrefab, allCells[WhichSpawn].transform);            // Instantiate a new prefab
            Debug.Log(4);

            // Pass the value of the fill object into to the fill value update function of the newly instantiated fill prefab
            Fill2048 tempFillComp = tempFill.GetComponent<Fill2048>();

            // Get the cell script from the current cell that we are instantiating this fill prefab onto
            allCells[WhichSpawn].GetComponent<Cell2048>().fill = tempFillComp;

            tempFillComp.FillValueUpdate(4);
        }
    }

    // This make no longer a chance of spawning a fill object and only spawn ones with the value of 2
    public void StartSpawnFill()
    {
        int WhichSpawn = UnityEngine.Random.Range(0, allCells.Length);                              // Random which transform we want to instantiate a new fill object

        // Check if the spawn point already has a child's object
        if (allCells[WhichSpawn].transform.childCount != 0)
        {
            Debug.Log(allCells[WhichSpawn].name + " is already filled");
            SpawnFill();
            return;
        }
            GameObject tempFill = Instantiate(fillPrefab, allCells[WhichSpawn].transform);            // Instantiate a new prefab
            Debug.Log(2);

            // Pass the value of the fill object into to the fill value update function of the newly instantiated fill prefab
            Fill2048 tempFillComp = tempFill.GetComponent<Fill2048>();

            // Get the cell script from the current cell that we are instantiating this fill prefab onto
            allCells[WhichSpawn].GetComponent<Cell2048>().fill = tempFillComp;

            tempFillComp.FillValueUpdate(2);
    }

    public void ScoreUpdate(int scoreIn)
    {
        myScore += scoreIn;
        scoreDisplay.text = myScore.ToString();
    }

    public void GameOverCheck()
    {
        isGameOver++;
        if(isGameOver >= 16)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
