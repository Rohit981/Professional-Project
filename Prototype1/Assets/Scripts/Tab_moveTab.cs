using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TabSlideDirection { Horizontal, Vertical };


public class Tab_moveTab : MonoBehaviour
{
    private const float MAX_OFFSET = 5;

    // Inspector
    [Header("Tab settings")]
    [SerializeField] public TabSlideDirection slideDirection;
    [SerializeField, Range(0, -MAX_OFFSET)] private float limitOffset1 = -1;
    [SerializeField, Range(0, MAX_OFFSET)] private float limitOffset2 = 1;
    [Tooltip("How fast the tab moves relative to mouse movement.")]
    [SerializeField] private float mouseSensitivity = 10;
    [Tooltip("How fast the tab lerps to its final position. Anything beyond 0.3 is probably going to be too fast for the human eye to notice.")]
    [SerializeField, Range(0.01f, 0.5f)] private float lerpSpeed = 0.15f;

    private bool selected = false;
    
    // these are used to draw the gizmos and to check if we're going beyond boundaries
    private Vector3 limit1;
    private Vector3 limit2;

    // An easing function will be used to slowly move tab toward the target
    private Vector3 lerpTargetPosition;

    // This runs when the mouse is hovering over the Tab
    void OnMouseOver()
    {
        // if left mouse is clicked when hovering on the Tab, activate selection
        if (Input.GetMouseButtonDown(0))
        {
            selected = true;
        }
    }

    // draw a vertical gizmo line
    private void drawVerticalGizmoLine(Vector3 lineStart)
    {
        Vector3 lineEnd = lineStart;
        lineEnd.y = 20;
        Gizmos.DrawLine(lineStart, lineEnd);
    }

    // update limits positions based on the object's position
    private void updateGizmosPositions()
    {
        limit1 = transform.position;
        limit2 = transform.position;
        switch (slideDirection)
        {
            case TabSlideDirection.Horizontal:
                limit1.x += limitOffset1;
                limit2.x += limitOffset2;
                break;

            case TabSlideDirection.Vertical:
                limit1.z += limitOffset1;
                limit2.z += limitOffset2;
                break;
        }
    }

    // draw gizmos for limits and object centre. only update limits' position if the Application is not running (edit mode)
    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
        {
            updateGizmosPositions();
        }
        Gizmos.color = Color.red;
        drawVerticalGizmoLine(limit1);
        drawVerticalGizmoLine(limit2);
        Gizmos.color = Color.blue;
        drawVerticalGizmoLine(transform.position);

    }

    // Use this for initialization
    void Start()
    {
        updateGizmosPositions();
        lerpTargetPosition = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            // if mouse left click is not being held down, release selection
            if (!Input.GetMouseButton(0))
                selected = false;

            float mouseMovement;
            switch (slideDirection)
            {
                case TabSlideDirection.Horizontal:
                    mouseMovement = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
                    lerpTargetPosition += new Vector3(mouseMovement, 0, 0);
                    if (lerpTargetPosition.x < limit1.x) lerpTargetPosition.x = limit1.x;
                    if (lerpTargetPosition.x > limit2.x) lerpTargetPosition.x = limit2.x;
                    break;

                case TabSlideDirection.Vertical:
                    mouseMovement = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
                    lerpTargetPosition += new Vector3(0, 0, mouseMovement);
                    if (lerpTargetPosition.z < limit1.z) lerpTargetPosition.z = limit1.z;
                    if (lerpTargetPosition.z > limit2.z) lerpTargetPosition.z = limit2.z;
                    break;
            }
        }

        if (lerpTargetPosition != transform.position)
        {
            switch (slideDirection)
            {
                case TabSlideDirection.Horizontal:
                    transform.position = new Vector3(Mathf.Lerp(transform.position.x, lerpTargetPosition.x, lerpSpeed), transform.position.y, transform.position.z); break;
                case TabSlideDirection.Vertical:
                    transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Lerp(transform.position.z, lerpTargetPosition.z, lerpSpeed)); break;
            }
        }
    }
}
