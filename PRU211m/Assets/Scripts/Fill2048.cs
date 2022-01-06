using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fill2048 : MonoBehaviour
{
    public int value;
    [SerializeField] Text valueDisplay;
    [SerializeField] float speed;

    bool hasCombine;    // Use to set fill object display ( destroy!!!!!!)

    public void FillValueUpdate(int valueIn)
    {
        value = valueIn;

        // Update value display
        valueDisplay.text = value.ToString();
    }

    // Moving the fill object
    private void Update()
    {
        if(transform.localPosition != Vector3.zero)
        {
            hasCombine = false;

            // Update the local position of this fill object
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, speed * Time.deltaTime);
        }

        // If hasCombine == false then want to check see if the first child of our current parent does not equal this current fill object
        else if (hasCombine == false)
        {
            // If this if statement is true then we want to destroy that other child object
            if (transform.parent.GetChild(0) != this.transform)
            {
                Destroy(transform.parent.GetChild(0).gameObject);
            }
            hasCombine = true;
        }
    }

    // Double the value of the fill object that's moving
    public void Double()
    {
        value *= 2;
        GameController.instance.ScoreUpdate(value);
        valueDisplay.text = value.ToString();
    }
}
