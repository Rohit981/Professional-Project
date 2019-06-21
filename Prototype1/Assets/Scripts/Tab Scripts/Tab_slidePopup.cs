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
    [SerializeField] private Transform slideMe;
    [Tooltip("How fast the popup moves relative to tab movement")]
    [SerializeField] private float movementSpeed = 1.0f;
    // --------------------------------------------------------------------------------------------------------------------------------------- INSPECTOR INTERFACE END

    private Tab_operateTab operateTabScript;
    private float tabMovement;

    private void Start()
    {
        operateTabScript = GetComponent<Tab_operateTab>();
        if (operateTabScript == null)
        {
            Debug.Log("The object this script is attached to needs to have a script that operates the tab!");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        tabMovement = operateTabScript.getTabMovement();
        if (tabMovement != 0)
        {
            switch (slideDirection)
            {
                case PopupSlideDirection.Horizontal:
                    slideMe.Translate(new Vector3(tabMovement * movementSpeed, 0, 0));
                    break;
                case PopupSlideDirection.Vertical:
                    slideMe.Translate(new Vector3(0, 0, tabMovement * movementSpeed));
                    break;
            }
        }
    }
}
