using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab_slideTab : BASECLASS_Tab_operateTab
{
    // ------------------------------------------------------------------------------------------------------ INSPECTOR INTERFACE - YOU CAN SAFELY TWEAK THESE VALUES

    // This is the max range value for the tab movement that will be suggested in the inspector
    private const float MAX_OFFSET = 5;

    [Header("")]
    [SerializeField] public TabInputDirection inputDirection = TabInputDirection.Horizontal;
    [SerializeField, Range(0, -MAX_OFFSET)] private float limitOffset1 = -1;
    [SerializeField, Range(0, MAX_OFFSET)] private float limitOffset2 = 1;
    [Tooltip("How far the tab moves relative to mouse movement.")]
    [SerializeField] private float mouseSensitivity = 10;
    [Tooltip("How fast the tab interpolates to its final position. Anything beyond 0.3 is probably going to be too fast for the human eye to notice.")]
    [SerializeField, Range(0.01f, 0.5f)] private float lerpSpeed = 0.2f;

    // --------------------------------------------------------------------------------------------------------------------------------------- INSPECTOR INTERFACE END
    
    private float mouseMovement;

    // these are used to draw the gizmos and to check if we're going beyond boundaries
    private Vector3 limit1;
    private Vector3 limit2;

    // An easing function will be used to slowly move tab toward the target
    private Vector3 lerpTargetPosition;

    // draw a vertical gizmo line
    void drawVerticalGizmoLine(Vector3 lineStart)
    {
        Vector3 lineEnd = lineStart;
        lineEnd.y = 20;
        Gizmos.DrawLine(lineStart, lineEnd);
    }

    // update limits positions based on the object's position
    void updateLimitsPositions()
    {
        limit1 = transform.position;
        limit2 = transform.position;
        
        switch (inputDirection)
        {
            case TabInputDirection.Horizontal:
                limit1.x += limitOffset1;
                limit2.x += limitOffset2;
                break;

            case TabInputDirection.Vertical:
                limit1.z += limitOffset1;
                limit2.z += limitOffset2;
                break;
        }
    }

    // draw gizmos for limits and object centre. only update limits' position if the Application is not running (edit mode)
    void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
        {
            updateLimitsPositions();
        }
        Gizmos.color = Color.red;
        drawVerticalGizmoLine(limit1);
        drawVerticalGizmoLine(limit2);
        Gizmos.color = Color.blue;
        drawVerticalGizmoLine(transform.position);

    }

    // Use this for initialization
    new void Start()
    {
        base.Start();
        updateLimitsPositions();
        lerpTargetPosition = transform.position;
    }


    // Update is called once per frame
    new void Update()
    {
        base.Update();

        if (selected) updateLerpTargetPosition();

        updatePosition();
    }

    private void updatePosition()
    {
        if (lerpTargetPosition != transform.position)
        {
            switch (inputDirection)
            {
                case TabInputDirection.Horizontal:
                    float lerpedValueX = Mathf.Lerp(transform.position.x, lerpTargetPosition.x, lerpSpeed);
                    tabMovement = lerpedValueX - transform.position.x;
                    transform.position = new Vector3(lerpedValueX, transform.position.y, transform.position.z);
                    break;

                case TabInputDirection.Vertical:
                    float lerpedValueZ = Mathf.Lerp(transform.position.z, lerpTargetPosition.z, lerpSpeed);
                    tabMovement = lerpedValueZ - transform.position.z;
                    transform.position = new Vector3(transform.position.x, transform.position.y, lerpedValueZ);
                    break;
            }
        }
    }

    private void updateLerpTargetPosition()
    {
        switch (inputDirection)
        {
            case TabInputDirection.Horizontal:
                mouseMovement = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
                lerpTargetPosition += new Vector3(mouseMovement, 0, 0);
                if (lerpTargetPosition.x < limit1.x) lerpTargetPosition.x = limit1.x;
                if (lerpTargetPosition.x > limit2.x) lerpTargetPosition.x = limit2.x;
                break;

            case TabInputDirection.Vertical:
                mouseMovement = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
                lerpTargetPosition += new Vector3(0, 0, mouseMovement);
                if (lerpTargetPosition.z < limit1.z) lerpTargetPosition.z = limit1.z;
                if (lerpTargetPosition.z > limit2.z) lerpTargetPosition.z = limit2.z;
                break;
        }
    }
}
