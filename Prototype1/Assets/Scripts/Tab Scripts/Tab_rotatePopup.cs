using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab_rotatePopup : MonoBehaviour
{
    // ------------------------------------------------------------------------------------------------------ INSPECTOR INTERFACE - YOU CAN SAFELY TWEAK THESE VALUES
    [Header("")]
    [SerializeField] private Transform rotateMe;
    [Tooltip("You probably want to create an empty child object for the popup and set it at the position you want the object to rotate around. Alternatively you could rotate around an existing object.")]
    [SerializeField] private Transform rotateAroundMe;
    [Tooltip("How fast the popup rotates relative to tab movement")]
    [SerializeField] private float rotationSpeed = 1.0f;

    // this is so that we don't have to put really high values in the inspector
    private const float rotationSpeedMultiplier = 50;
    // --------------------------------------------------------------------------------------------------------------------------------------- INSPECTOR INTERFACE END


    private Tab_operateTab operateTabScript;
    private float tabMovement;

    private void Start()
    {
        // retrieve the component from which we will read the input
        operateTabScript = GetComponent<Tab_operateTab>();
        if (operateTabScript == null)
        {
            Debug.Log("The object this script is attached to needs to have a script that operates the tab!");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // how much the tab moved/rotated since last frame
        tabMovement = operateTabScript.tabMovement;

        // if there has been any movement since last frame
        if (tabMovement != 0)
        {
            if (rotateMe == null) Debug.Log("You attached a rotate pop up script but you did not give me any pop up to rotate! :(");
            else
            {
                if (rotateAroundMe == null) Debug.Log("I need a position to rotate around! Read the tooltip for Rotate Around Me! :)");
                // slide all the transforms in the appropriate direction (selected in the inspector)
                else
                {
                    rotateMe.RotateAround(rotateAroundMe.position, Vector3.up, tabMovement * rotationSpeed * rotationSpeedMultiplier);
                }
            }
        }
    }
}
