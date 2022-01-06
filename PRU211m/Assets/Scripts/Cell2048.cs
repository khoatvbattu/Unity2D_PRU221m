using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell2048 : MonoBehaviour
{
    // Hold preferencs to the neighbooring cells of our current cell
    public Cell2048 up;
    public Cell2048 down;
    public Cell2048 left;
    public Cell2048 right;

    private void OnEnable()
    {
        GameController.slide += OnSlide;        // Subscribe function
    }

    public void OnDisable()
    {
        GameController.slide -= OnSlide;        // Unsubscribe function
    }

    private void OnSlide(string whatWasSent)
    {
        Debug.Log(whatWasSent);
    }
}
