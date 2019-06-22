using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab_slideTab : Tab_operateTab
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

    /*
    [SerializeField] private bool interpolationActive;
    [Tooltip("How fast the tab interpolates to its final position. Anything beyond 0.2 is probably going to be too fast for the human eye to notice.")]
    [SerializeField, Range(0.01f, 0.5f)] private float interpolationSpeed = 0.5f;
    */

    // --------------------------------------------------------------------------------------------------------------------------------------- INSPECTOR INTERFACE END

    // these are used to draw the gizmos and to check if we're going beyond boundaries
    private Vector3 limit1;
    private Vector3 limit2;

    /*
    // An easing function will be used to slowly move tab toward the target
    private Vector3 interpolateFrom;
    private Vector3 interpolateTo;
    float t = 0.0f;
    */

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
        Gizmos.DrawRay(limit1, Vector3.up * 5);
        Gizmos.DrawRay(limit2, Vector3.up * 5);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, Vector3.up * 5);
    }

    // Use this for initialization
    new void Start()
    {
        base.Start();
        updateLimitsPositions();

        /*
        if (interpolationActive)
        {
            interpolateFrom = transform.position;
            interpolateTo = transform.position;
        }
        */
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        if (selected)
        {
            UpdatePosition();
        }

    }

    private void UpdatePosition()
    {
        tabMovement = 0;
        switch (inputDirection)
        {
            case TabInputDirection.Horizontal:
                tabMovement = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
                transform.position += new Vector3(tabMovement, 0, 0);
                // check horizontal limits
                if (transform.position.x < limit1.x)
                {
                    transform.position = limit1;
                    tabMovement = 0;
                }
                if (transform.position.x > limit2.x)
                {
                    transform.position = limit2;
                    tabMovement = 0;
                }
                TabMovementPercentage = (transform.position.x - limit1.x) / (limit2.x - limit1.x);
                
                break;

            case TabInputDirection.Vertical:
                if (transform.position.z >= limit1.z && transform.position.z <= limit2.z)
                {
                    tabMovement = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
                    transform.position += new Vector3(0, 0, tabMovement);
                    // check vertical limits
                    if (transform.position.z < limit1.z) transform.position = limit1;
                    if (transform.position.z > limit2.z) transform.position = limit2;
                    TabMovementPercentage = (transform.position.z - limit1.z) / (limit2.z - limit1.z);
                }
                break;
        }
    }
    


    /*
    private void updatePosition()
    {
        if (interpolateTo != transform.position)
        {
            switch (inputDirection)
            {
                case TabInputDirection.Horizontal:
                    float lerpedValueX = Mathf.Lerp(interpolateFrom.x, interpolateTo.x, t);
                    tabMovement = lerpedValueX - transform.position.x;
                    transform.position = new Vector3(lerpedValueX, transform.position.y, transform.position.z);
                    break;

                case TabInputDirection.Vertical:
                    float lerpedValueZ = Mathf.Lerp(interpolateFrom.z, interpolateTo.z, t);
                    tabMovement = lerpedValueZ - transform.position.z;
                    transform.position = new Vector3(transform.position.x, transform.position.y, lerpedValueZ);
                    break;
            }

            t += interpolationSpeed * Time.deltaTime;
        }
        //TabMovementPercentage = tabMovement / limitOffset2 - limitOffset1;
    }

    private void updateInterpolationStartEndValues()
    {
        interpolateFrom = transform.position;
        switch (inputDirection)
        {
            case TabInputDirection.Horizontal:
                mouseMovement = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
                interpolateTo += new Vector3(mouseMovement, 0, 0);
                if (interpolateTo.x < limit1.x) interpolateTo.x = limit1.x;
                if (interpolateTo.x > limit2.x) interpolateTo.x = limit2.x;
                break;

            case TabInputDirection.Vertical:
                mouseMovement = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
                interpolateTo += new Vector3(0, 0, mouseMovement);
                if (interpolateTo.z < limit1.z) interpolateTo.z = limit1.z;
                if (interpolateTo.z > limit2.z) interpolateTo.z = limit2.z;
                break;
        }
    }
    */
}

