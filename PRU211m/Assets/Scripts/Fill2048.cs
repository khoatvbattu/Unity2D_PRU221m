using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fill2048 : MonoBehaviour
{
    public int value;
    [SerializeField] Text valueDisplay;
    [SerializeField] float speed;
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
            // Update the local position of this fill object
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, speed * Time.deltaTime);
        }
    }
}
