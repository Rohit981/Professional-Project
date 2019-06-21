using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PopupSlideDirection
{
    Horizontal,
    Vertical
}

public class Tab_slidePopup : Tab_operatePopup
{
    // ------------------------------------------------------------------------------------------------------ INSPECTOR INTERFACE - YOU CAN SAFELY TWEAK THESE VALUES
    [Header("")]
    [SerializeField] public PopupSlideDirection slideDirection = PopupSlideDirection.Horizontal;
    [Tooltip("You need to drag the parent popUp in here so that both the popup and the shadow will move")]
    [SerializeField] private List<Transform> slideMe;
    [Tooltip("How fast the popup moves relative to tab movement")]
    [SerializeField] private float movementSpeed = 1.0f;
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
        tabMovement = operateTabScript.getTabMovement();

        // if there has been any movement since last frame
        if (tabMovement != 0)
        {
            if (slideMe.Count == 0) Debug.Log("You attached a slide pop up script but you did not give me any pop up to slide! :(");
            // slide all the transforms in the appropriate direction (selected in the inspector)
            else
            {
                foreach (Transform popupTransform in slideMe)
                {
                    switch (slideDirection)
                    {
                        case PopupSlideDirection.Horizontal: popupTransform.Translate(new Vector3(tabMovement * movementSpeed, 0, 0)); break;
                        case PopupSlideDirection.Vertical: popupTransform.Translate(new Vector3(0, 0, tabMovement * movementSpeed)); break;
                    }
                }
            }
        }
    }
}