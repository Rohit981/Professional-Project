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
    [SerializeField] private List<Transform> slideMe;

    [Header("")]
    [SerializeField] public bool fixedLimits;
    private const float MAX_OFFSET = 5;
    [ConditionalHide("fixedLimits", true, false, 0, -MAX_OFFSET)]
    [SerializeField] private float limitOffset1 = -1;
    [ConditionalHide("fixedLimits", true, false, 0, MAX_OFFSET)]
    [SerializeField] private float limitOffset2 = 1;

    [ConditionalHide("fixedLimits", true, true)]
    [Tooltip("How fast the popup moves relative to tab movement")]
    [SerializeField] private float movementSpeed = 1.0f;
    // --------------------------------------------------------------------------------------------------------------------------------------- INSPECTOR INTERFACE END

    private Tab_operateTab operateTabScript;
    private float tabMovement;
    private float tabMovementPercentage;

    // these are used to draw the gizmos and to check if we're going beyond boundaries
    private Vector3 limit1;
    private Vector3 limit2;

    // update limits positions based on the object's position
    void updateLimitsPositions()
    {
        limit1 = slideMe[0].position;
        limit2 = slideMe[0].position;
        switch (slideDirection)
        {
            case PopupSlideDirection.Horizontal:
                limit1.x += limitOffset1;
                limit2.x += limitOffset2;
                break;

            case PopupSlideDirection.Vertical:
                limit1.z += limitOffset1;
                limit2.z += limitOffset2;
                break;
        }
    }

    // draw gizmos for limits and object centre. only update limits' position if the Application is not running (edit mode)
    void OnDrawGizmosSelected()
    {
        if (fixedLimits)
        {
            if (!Application.isPlaying)
            {
                updateLimitsPositions();
            }
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(limit1, Vector3.up * 5);
            Gizmos.DrawRay(limit2, Vector3.up * 5);
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(slideMe[0].position, Vector3.up * 5);
        }
    }

    private void Start()
    {
        if (fixedLimits)
        {
            updateLimitsPositions();
        }
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
            if (slideMe.Count == 0) Debug.Log("You attached a slide pop up script but you did not give me any pop up to slide! :(");
            // slide all the transforms in the appropriate direction (selected in the inspector)
            else
            {
                foreach (Transform popupTransform in slideMe)
                {
                    if (fixedLimits)
                    {
                        tabMovementPercentage = operateTabScript.TabMovementPercentage;
                        switch (slideDirection)
                        {
                            case PopupSlideDirection.Horizontal:
                                popupTransform.transform.position = new Vector3(limit1.x + (tabMovementPercentage * (limit2.x - limit1.x)), popupTransform.position.y, popupTransform.position.z);
                                break;
                            case PopupSlideDirection.Vertical:
                                popupTransform.transform.position = new Vector3(popupTransform.position.x, popupTransform.position.y, limit1.z + (tabMovementPercentage * (limit2.z - limit1.z)));
                                break;
                        }
                    }
                    else
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
}