using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab_rotateTab : BASECLASS_Tab_operateTab
{
    // ------------------------------------------------------------------------------------------------------ INSPECTOR INTERFACE - YOU CAN SAFELY TWEAK THESE VALUES

    // This is the max range value for the tab movement that will be suggested in the inspector
    private const float MAX_OFFSET = 180;

    [Header("")]
    [SerializeField] public TabInputDirection inputDirection = TabInputDirection.Horizontal;
    [SerializeField, Range(0, -MAX_OFFSET)] private float limitOffset1 = -45;
    [SerializeField, Range(0, MAX_OFFSET)] private float limitOffset2 = 45;
    [Tooltip("How much the tab rotates relative to mouse movement.")]
    [SerializeField] private float mouseSensitivity = 1;
    [Tooltip("How fast the tab interpolates to its final position. Anything beyond 0.3 is probably going to be too fast for the human eye to notice.")]
    [SerializeField, Range(0.01f, 0.5f)] private float lerpSpeed = 0.15f;

    // --------------------------------------------------------------------------------------------------------------------------------------- INSPECTOR INTERFACE END

    private float mouseMovement;

    // An easing function will be used to slowly rotate tab towards the target
    private Vector3 lerpTargetRotation;

    // these are used to draw the gizmos and to check if we're going beyond boundaries
    private float limit1;
    private float limit2;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        updateLimitsPositions();
        lerpTargetRotation = transform.localEulerAngles;

        // ------------------------------------------------------- FIX ME! -------------- this is because mouse sensitivity would otherwise need really high values in the inspector
        mouseSensitivity = mouseSensitivity * 100;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        if (selected) updateLerpTargetPosition();

        updatePosition();
    }

    // update limits positions based on the object's position
    void updateLimitsPositions()
    {
        limit1 = transform.localEulerAngles.y + limitOffset1;
        limit2 = transform.localEulerAngles.y + limitOffset2;
    }

    private void updatePosition()
    {
        if (lerpTargetRotation != transform.localEulerAngles)
        {
            float lerpedValueY = Mathf.Lerp(transform.localEulerAngles.y, lerpTargetRotation.y, lerpSpeed);
            tabMovement = lerpedValueY - transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, lerpedValueY, transform.localEulerAngles.z);
        }
    }


    // draw gizmos. only update limits' position if the Application is not running (edit mode)
    void OnDrawGizmosSelected()
    {            
        // Convert the local coordinate values into world
        // coordinates for the matrix transformation.
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(Vector3.zero, Vector3.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Vector3.zero, new Vector3(Mathf.Cos(limitOffset1 * Mathf.Deg2Rad), 0, Mathf.Sin(limitOffset1 * Mathf.Deg2Rad)));
        Gizmos.DrawRay(Vector3.zero, new Vector3(Mathf.Cos(limitOffset2 * Mathf.Deg2Rad), 0, Mathf.Sin(limitOffset2 * Mathf.Deg2Rad)));
        //Vector3.forward + new Vector3(Mathf.Sin(limitOffset1), 1, 1));
        //Gizmos.DrawRay(transform.position + lineOffset, transform.forward + lineOffset + new Vector3(Mathf.Cos(limit2), 0, 0));

        //Vector3 lineOffset = new Vector3(0, 0.01f, 0);

        //if (!Application.isPlaying)
        //{
        //    updateLimitsPositions();
        //}

        //Gizmos.color = Color.blue;
        //Gizmos.DrawRay(transform.position + lineOffset, transform.forward + lineOffset);

    }

    private void updateLerpTargetPosition()
    {
        switch (inputDirection)
        {
            case TabInputDirection.Horizontal:
                mouseMovement = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
                break;

            case TabInputDirection.Vertical:
                mouseMovement = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
                break;
        }

        lerpTargetRotation += new Vector3(0, mouseMovement, 0);
        if (lerpTargetRotation.y < limit1) lerpTargetRotation.y = limit1;
        if (lerpTargetRotation.y > limit2) lerpTargetRotation.y = limit2;
    }
}
