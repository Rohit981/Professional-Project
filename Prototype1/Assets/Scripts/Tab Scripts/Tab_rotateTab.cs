using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab_rotateTab : Tab_operateTab
{
    // ------------------------------------------------------------------------------------------------------ INSPECTOR INTERFACE - YOU CAN SAFELY TWEAK THESE VALUES

    // This is the max range value for the tab movement that will be suggested in the inspector
    private const float MAX_OFFSET = 180;

    [Header("")]
    [SerializeField] public TabInputDirection inputDirection = TabInputDirection.Vertical;
    [SerializeField, Range(0, -MAX_OFFSET)] private float limitOffset1 = -45;
    [SerializeField, Range(0, MAX_OFFSET)] private float limitOffset2 = 45;
    [Tooltip("How far the tab moves relative to mouse movement.")]
    //[SerializeField] private float mouseSensitivity = 10;
    //[Tooltip("How fast the tab interpolates to its final position. Anything beyond 0.3 is probably going to be too fast for the human eye to notice.")]
    //[SerializeField, Range(0.01f, 0.5f)] private float lerpSpeed = 0.2f;

    // --------------------------------------------------------------------------------------------------------------------------------------- INSPECTOR INTERFACE END

    private float mouseMovement;

    // these are used to draw the gizmos and to check if we're going beyond boundaries
    private float limit1;
    private float limit2;

    // An easing function will be used to slowly move tab toward the target
    // private Vector3 lerpTargetPosition;

    // update limits positions based on the object's position
    void updateLimitsPositions()
    {
        limit1 = transform.rotation.y + limitOffset1;
        limit2 = transform.rotation.y + limitOffset2;
    }

    // draw gizmos for limits and object centre. only update limits' position if the Application is not running (edit mode)
    void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
        {
            updateLimitsPositions();
        }
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Vector3.zero, new Vector3(Mathf.Cos(limit1 * Mathf.Deg2Rad), 0, Mathf.Sin(limit1 * Mathf.Deg2Rad)));
        Gizmos.DrawRay(Vector3.zero, new Vector3(Mathf.Cos(limit2 * Mathf.Deg2Rad), 0, Mathf.Sin(limit2 * Mathf.Deg2Rad)));

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(Vector3.zero, Vector3.forward);

    }

    // Use this for initialization
    new void Start()
    {
        base.Start();
        updateLimitsPositions();
        //lerpTargetPosition = transform.position;
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
        
    }

    private void updateLerpTargetPosition()
    {
    }
}
