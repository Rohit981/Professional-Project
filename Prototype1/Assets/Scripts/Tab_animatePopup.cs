using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab_animatePopup : MonoBehaviour
{

    public enum TabSlideDirection { Horizontal, Vertical };

    [Header("Tab settings")]
    [SerializeField] private TabSlideDirection slideDirection;
    [SerializeField] private float mouseSensitivity = 10;
    [SerializeField] private float limitOffset1 = 2;
    [SerializeField] private float limitOffset2 = 2;

    [Header("Animation settings")]
    [SerializeField] private Animator popUpAnimator;
    [SerializeField] private Animator shadowAnimator;
    [SerializeField] private string animationName;
    [SerializeField] private float animationSpeed = 0.2f;

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

    private void drawVerticalGizmoLine(Vector3 lineStart)
    {
        Vector3 lineEnd = lineStart;
        lineEnd.y = 20;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(lineStart, lineEnd);
    }

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

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
        {
            updateGizmosPositions();
        }
        drawVerticalGizmoLine(limit1);
        drawVerticalGizmoLine(limit2);
    }

    // Use this for initialization
    void Start()
    {
        updateGizmosPositions();
    }

    private void playAnimations(float mouseMovement)
    {
        // modify animation's time value based on how much mouse has moved
        popUpAnimator.Play(animationName, 0, popUpAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime + (mouseMovement * animationSpeed));
        shadowAnimator.Play(animationName, 0, popUpAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime + (mouseMovement * animationSpeed));
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            // if mouse left click is not being held down, release selection
            if (!Input.GetMouseButton(0))
                selected = false;

            float mouseMovement = 0.0f;

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
                        playAnimations(mouseMovement);
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
                        playAnimations(mouseMovement);
                    }
                    break;
            }
            


        }
    }
}
