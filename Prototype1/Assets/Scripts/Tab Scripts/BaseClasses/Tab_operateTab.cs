using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab_operateTab : MonoBehaviour
{

    public enum TabInputDirection { Horizontal, Vertical };

    protected bool selected = false;

    // highlight object when selected
    private Renderer objectRenderer;
    private Color initialColor;
    private Color highlightColor = Color.red;

    // This runs when the mouse is hovering over the Tab
    void OnMouseOver()
    {
        if (!selected)
        {
            // if left mouse is clicked when hovering on the Tab, activate selection
            if (Input.GetMouseButtonDown(0))
            {
                selected = true;
                objectRenderer.material.color = highlightColor;
            }
        }
    }

    protected void Update()
    {
        if (selected)
        {
            // if mouse left click is not being held down, release selection
            if (!Input.GetMouseButton(0))
            {
                selected = false;
                objectRenderer.material.color = initialColor;
            }
        }
    }

    protected void Start()
    {
        selected = false;
        objectRenderer = GetComponent<Renderer>();
        initialColor = objectRenderer.material.color;
    }


    public float tabMovement { get; protected set; }
    public float TabMovementPercentage { get; protected set; }

}
