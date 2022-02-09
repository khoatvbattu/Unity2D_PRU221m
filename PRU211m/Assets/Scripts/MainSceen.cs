using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceen : MonoBehaviour
{
    [SerializeField] GameObject mainScreen;
    [SerializeField] GameObject game2048;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void display2048()
    {
        mainScreen.SetActive(false);
        game2048.SetActive(true);
    }
}
