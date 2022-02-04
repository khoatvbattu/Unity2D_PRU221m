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
        CellCheck();    
        Debug.Log(whatWasSent);

        // If it's equal to "a" that means we're shifting all the fill objects in the left direction
        if (whatWasSent == "a")
        {
            // If it's not equal null that means our current cell is not one of the cells on the left column
            if (left != null)
            {
                return;
            }
            // Temp variable
            Cell2048 currentCell = this;
            SlideLeft(currentCell);
        }

        // If it's equal to "d" that means we're shifting all the fill objects in the right direction
        if (whatWasSent == "d")
        {
            // If it's not equal null that means our current cell is not one of the cells on the right column
            if (right != null)
            {
                return;
            }
            // Temp variable
            Cell2048 currentCell = this;
            SlideRight(currentCell);
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
            // If it's not equal null that means our current cell is not one of the cells on the bottom row
            if (down != null)
            {
                return;
            }
            // Temp variable
            Cell2048 currentCell = this;
            SlideDown(currentCell);
        }

        GameController.ticker++;

        // Check to see if our ticker variable is equal to 4
        if (GameController.ticker == 4)
        {
            GameController.instance.SpawnFill();
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
                    nextCell.fill.Double();

                    // Set the parent of the next cell's fill to be the current cell
                    nextCell.fill.transform.parent = currentCell.transform;

                    // Anytime we're changing the parent of a fill object we need to set the fill variable of that cell to be the fill object that we're moving
                    currentCell.fill = nextCell.fill;
                    nextCell.fill = null;
                }
                else if(currentCell.down.fill != nextCell.fill)
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

    // Recursive function that will execute on each cell as we traverse up the column
    void SlideDown(Cell2048 currentCell)
    {
        if (currentCell.up == null)
        {
            return;
        }

        // Check to see if our current cell is filled or not
        if (currentCell.fill != null)
        {
            // Get the next available cell that is filled
            Cell2048 nextCell = currentCell.up;

            // If both conditions are true then we want to get the next available cell
            // To break this loop it means that we're either at the top of our column or we've ran into a cell that is filled or both
            while (nextCell.up != null && nextCell.fill == null)
            {
                nextCell = nextCell.up;
            }

            // Check if we are filled
            if (nextCell.fill != null)
            {
                // Check to see if the current cell has the same value as this next cell
                if (currentCell.fill.value == nextCell.fill.value)
                {
                    nextCell.fill.Double();

                    // Set the parent of the next cell's fill to be the current cell
                    nextCell.fill.transform.parent = currentCell.transform;

                    // Anytime we're changing the parent of a fill object we need to set the fill variable of that cell to be the fill object that we're moving
                    currentCell.fill = nextCell.fill;
                    nextCell.fill = null;
                }
                else if(currentCell.up.fill != nextCell.fill)
                {
                    Debug.Log("!!!doubled");

                    // Set the parent of our next fill to be the next cell from our current cell
                    nextCell.fill.transform.parent = currentCell.up.transform;

                    // Anytime we're changing the parent of a fill object we need to set the fill variable of that cell to be the fill object that we're moving
                    currentCell.up.fill = nextCell.fill;
                    nextCell.fill = null;
                }
            }
            /*
             * The else for this If statement that means We are at the top of our column and that cell is empty
             */
        }
        /*
         * This else statement means that the current cell is empty
         */
        else
        {
            // Get the next available cell that is filled
            Cell2048 nextCell = currentCell.up;

            // If both conditions are true then we want to get the next available cell
            // To break this loop it means that we're either at the top of our column or we've ran into a cell that is filled or both
            while (nextCell.up != null && nextCell.fill == null)
            {
                Debug.Log("here");
                nextCell = nextCell.up;
            }

            // Check if we are filled
            if (nextCell.fill != null)
            {
                nextCell.fill.transform.parent = currentCell.transform;

                // Anytime we're changing the parent of a fill object we need to set the fill variable of that cell to be the fill object that we're moving
                currentCell.fill = nextCell.fill;
                nextCell.fill = null;

                // Recurse through this function again for the same current cell
                SlideDown(currentCell);
                Debug.Log("Slide to empty");
            }

            // Code that we add before this IF statement will be executed on each cell as we traverse up the column
            if (currentCell.up == null)
            {
                return;
            }
            SlideDown(currentCell.up);
        }
    }

    // Recursive function that will execute on each cell as we traverse left the column
    void SlideRight(Cell2048 currentCell)
    {
        if (currentCell.left == null)
        {
            return;
        }

        // Check to see if our current cell is filled or not
        if (currentCell.fill != null)
        {
            // Get the next available cell that is filled
            Cell2048 nextCell = currentCell.left;

            // If both conditions are true then we want to get the next available cell
            // To break this loop it means that we're either at the left of our column or we've ran into a cell that is filled or both
            while (nextCell.left != null && nextCell.fill == null)
            {
                nextCell = nextCell.left;
            }

            // Check if we are filled
            if (nextCell.fill != null)
            {
                // Check to see if the current cell has the same value as this next cell
                if (currentCell.fill.value == nextCell.fill.value)
                {
                    nextCell.fill.Double();

                    // Set the parent of the next cell's fill to be the current cell
                    nextCell.fill.transform.parent = currentCell.transform;

                    // Anytime we're changing the parent of a fill object we need to set the fill variable of that cell to be the fill object that we're moving
                    currentCell.fill = nextCell.fill;
                    nextCell.fill = null;
                }
                else if (currentCell.left.fill != nextCell.fill)
                {
                    Debug.Log("!!!doubled");

                    // Set the parent of our next fill to be the next cell from our current cell
                    nextCell.fill.transform.parent = currentCell.left.transform;

                    // Anytime we're changing the parent of a fill object we need to set the fill variable of that cell to be the fill object that we're moving
                    currentCell.left.fill = nextCell.fill;
                    nextCell.fill = null;
                }
            }
            /*
             * The else for this If statement that means We are at the left of our column and that cell is empty
             */
        }
        /*
         * This else statement means that the current cell is empty
         */
        else
        {
            // Get the next available cell that is filled
            Cell2048 nextCell = currentCell.left;

            // If both conditions are true then we want to get the next available cell
            // To break this loop it means that we're either at the left of our column or we've ran into a cell that is filled or both
            while (nextCell.left != null && nextCell.fill == null)
            {
                Debug.Log("here");
                nextCell = nextCell.left;
            }

            // Check if we are filled
            if (nextCell.fill != null)
            {
                nextCell.fill.transform.parent = currentCell.transform;

                // Anytime we're changing the parent of a fill object we need to set the fill variable of that cell to be the fill object that we're moving
                currentCell.fill = nextCell.fill;
                nextCell.fill = null;

                // Recurse through this function again for the same current cell
                SlideRight(currentCell);
                Debug.Log("Slide to empty");
            }

            // Code that we add before this IF statement will be executed on each cell as we traverse left the column
            if (currentCell.left == null)
            {
                return;
            }
            SlideRight(currentCell.left);
        }
    }

    // Recursive function that will execute on each cell as we traverse right the column
    void SlideLeft(Cell2048 currentCell)
    {
        if (currentCell.right == null)
        {
            return;
        }

        // Check to see if our current cell is filled or not
        if (currentCell.fill != null)
        {
            // Get the next available cell that is filled
            Cell2048 nextCell = currentCell.right;

            // If both conditions are true then we want to get the next available cell
            // To break this loop it means that we're either at the right of our column or we've ran into a cell that is filled or both
            while (nextCell.right != null && nextCell.fill == null)
            {
                nextCell = nextCell.right;
            }

            // Check if we are filled
            if (nextCell.fill != null)
            {
                // Check to see if the current cell has the same value as this next cell
                if (currentCell.fill.value == nextCell.fill.value)
                {
                    nextCell.fill.Double();

                    // Set the parent of the next cell's fill to be the current cell
                    nextCell.fill.transform.parent = currentCell.transform;

                    // Anytime we're changing the parent of a fill object we need to set the fill variable of that cell to be the fill object that we're moving
                    currentCell.fill = nextCell.fill;
                    nextCell.fill = null;
                }
                else if (currentCell.right.fill != nextCell.fill)
                {
                    Debug.Log("!!!doubled");

                    // Set the parent of our next fill to be the next cell from our current cell
                    nextCell.fill.transform.parent = currentCell.right.transform;

                    // Anytime we're changing the parent of a fill object we need to set the fill variable of that cell to be the fill object that we're moving
                    currentCell.right.fill = nextCell.fill;
                    nextCell.fill = null;
                }
            }
            /*
             * The else for this If statement that means We are at the right of our column and that cell is empty
             */
        }
        /*
         * This else statement means that the current cell is empty
         */
        else
        {
            // Get the next available cell that is filled
            Cell2048 nextCell = currentCell.right;

            // If both conditions are true then we want to get the next available cell
            // To break this loop it means that we're either at the right of our column or we've ran into a cell that is filled or both
            while (nextCell.right != null && nextCell.fill == null)
            {
                Debug.Log("here");
                nextCell = nextCell.right;
            }

            // Check if we are filled
            if (nextCell.fill != null)
            {
                nextCell.fill.transform.parent = currentCell.transform;

                // Anytime we're changing the parent of a fill object we need to set the fill variable of that cell to be the fill object that we're moving
                currentCell.fill = nextCell.fill;
                nextCell.fill = null;

                // Recurse through this function again for the same current cell
                SlideLeft(currentCell);
                Debug.Log("Slide to empty");
            }

            // Code that we add before this IF statement will be executed on each cell as we traverse right the column
            if (currentCell.right == null)
            {
                return;
            }
            SlideLeft(currentCell.right);
        }
    }

    // This function will execute on all 16 cells and so if our isGameOver variable reachs 16 then we know that the player can no longer move and display the game over panel
    void CellCheck()
    {
        // Look at each of the neighboring cells and see if the values of those cells are different than our current cell
        
        if(fill == null)
        {
            // If the current cell isn't filled then the player can still move
            return;
        }

        /*
         * These 4 if statements below means that our current cell is filled and blocked by another fill object with a different value on all 4 sides
         */

        // Check for the neighbor in the left direction
        // Do this because we want to make sure that there is a neighbor in the left direction
        if (left != null)
        {
            if (left.fill == null)
            {
                return;
            }
            if (left.fill.value == fill.value)
            {
                return;
            }
        }

        // Check for the neighbor in the right direction
        // Do this because we want to make sure that there is a neighbor in the right direction
        if (right != null)
        {
            if (right.fill == null)
            {
                return;
            }
            if (right.fill.value == fill.value)
            {
                return;
            }
        }

        // Check for the neighbor in the up direction
        // Do this because we want to make sure that there is a neighbor in the up direction
        if (up != null)
        {
            if(up.fill == null)
            {
                return;
            }
            if(up.fill.value == fill.value)
            {
                return;
            }
        }

        // Check for the neighbor in the down direction
        // Do this because we want to make sure that there is a neighbor in the down direction
        if (down != null)
        {
            if (down.fill == null)
            {
                return;
            }
            if (down.fill.value == fill.value)
            {
                return;
            }
        }

        GameController.instance.GameOverCheck();
    }
}
