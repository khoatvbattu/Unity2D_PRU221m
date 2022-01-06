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

    public Fill2048 fill;   // This variable will be initialized when we have a fill object instantiated in this cell

    // To handle the movement of the fill object on our 2048 grid, we need to work backwards and
    // that's because it's easier to work from the cell that we want to move into than the cell that we're currently in

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

        // If it's equal to "a" that means we're shifting all the fill objects in the left direction
        if (whatWasSent == "a")
        {

        }

        // If it's equal to "d" that means we're shifting all the fill objects in the right direction
        if (whatWasSent == "d")
        {

        }

        // If it's equal to "w" that means we're shifting all the fill objects in the up direction
        if (whatWasSent == "w")
        {
            // If it's not equal null that means our current cell is not one of the cells on the top row
            if (up != null)
            {
                return;
            }
            // Temp variable
            Cell2048 currentCell = this;
            SlideUp(currentCell);
        }

        // If it's equal to "s" that means we're shifting all the fill objects in the down direction
        if (whatWasSent == "s")
        {

        }
    }

    // Recursive function that will execute on each cell as we traverse down the column
    void SlideUp(Cell2048 currentCell)
    {
        if (currentCell.down == null)
        {
            return;
        }

        // Check to see if our current cell is filled or not
        if (currentCell.fill != null)
        {
            // Get the next available cell that is filled
            Cell2048 nextCell = currentCell.down;

            // If both conditions are true then we want to get the next available cell
            // To break this loop it means that we're either at the end of our column or we've ran into a cell that is filled or both
            while (nextCell.down != null && nextCell.fill == null)
            {
                nextCell = nextCell.down;
            }

            // Check if we are filled
            if (nextCell.fill != null)
            {
                // Check to see if the current cell has the same value as this next cell
                if (currentCell.fill.value == nextCell.fill.value)
                {
                    Debug.Log("doubled");

                    // Set the parent of the next cell's fill to be the current cell
                    nextCell.fill.transform.parent = currentCell.transform;

                    // Anytime we're changing the parent of a fill object we need to set the fill variable of that cell to be the fill object that we're moving
                    currentCell.fill = nextCell.fill;
                    nextCell.fill = null;
                }
                else
                {
                    Debug.Log("!!!doubled");

                    // Set the parent of our next fill to be the next cell from our current cell
                    nextCell.fill.transform.parent = currentCell.down.transform;

                    // Anytime we're changing the parent of a fill object we need to set the fill variable of that cell to be the fill object that we're moving
                    currentCell.down.fill = nextCell.fill;
                    nextCell.fill = null;
                }
            }
            /*
             * The else for this If statement that means We are at the end of our column and that cell is empty
             */
        }
        /*
         * This else statement means that the current cell is empty
         */
        else
        {
            // Get the next available cell that is filled
            Cell2048 nextCell = currentCell.down;

            // If both conditions are true then we want to get the next available cell
            // To break this loop it means that we're either at the end of our column or we've ran into a cell that is filled or both
            while (nextCell.down != null && nextCell.fill == null)
            {
                Debug.Log("here");
                nextCell = nextCell.down;
            }

            // Check if we are filled
            if (nextCell.fill != null)
            {
                nextCell.fill.transform.parent = currentCell.transform;

                // Anytime we're changing the parent of a fill object we need to set the fill variable of that cell to be the fill object that we're moving
                currentCell.fill = nextCell.fill;
                nextCell.fill = null;

                // Recurse through this function again for the same current cell
                SlideUp(currentCell);
                Debug.Log("Slide to empty");
            }

            // Code that we add before this IF statement will be executed on each cell as we traverse down the column
            if (currentCell.down == null)
            {
                return;
            }
            SlideUp(currentCell.down);
        }
    }
}
