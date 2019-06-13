using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TabSlideDirection { Horizontal, Vertical };

public class Tab_moveTab : MonoBehaviour {
    

    // Inspector
    [Header("Tab settings")]
    [SerializeField] public TabSlideDirection slideDirection;
    [Tooltip("How fast the tab moves relative to mouse movement")]
    [SerializeField] private float mouseSensitivity = 10;
    [SerializeField] private float limitOffset1 = 2;
    [SerializeField] private float limitOffset2 = 2;

    private bool selected = false;


    // these are used to draw the gizmos and to check if we're going beyond boundaries
    private Vector3 limit1;
    private Vector3 limit2;

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
                limit1.x -= limitOffset1;
                limit2.x += limitOffset2;
                break;

            case TabSlideDirection.Vertical:
                limit1.z -= limitOffset1;
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
                    // if tab is not going beyond boundaries
                    if (mouseMovement < 0 && transform.position.x >= limit1.x
                        ||
                        mouseMovement > 0 && transform.position.x <= limit2.x)
                    {
                        // add left right mouse movement to the X value of the object's transform
                        transform.position += new Vector3(mouseMovement, 0, 0);
                    }
                    break;

                case TabSlideDirection.Vertical:
                    mouseMovement = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
                    // if tab is not going beyond boundaries
                    if (mouseMovement > 0 && transform.position.z <= limit2.z
                        ||
                        mouseMovement < 0 && transform.position.z >= limit1.z)
                    {
                        // add up down mouse movement to the Z value of the object's transform
                        transform.position += new Vector3(0, 0, mouseMovement);
                    }
                    break;
            }
            

        }
    }
}
