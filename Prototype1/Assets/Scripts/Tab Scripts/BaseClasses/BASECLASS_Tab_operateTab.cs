using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BASECLASS_Tab_operateTab : MonoBehaviour {
    
    public enum TabInputDirection { Horizontal, Vertical };


    protected bool selected = false;

    // This runs when the mouse is hovering over the Tab
    void OnMouseOver()
    {
        // if left mouse is clicked when hovering on the Tab, activate selection
        if (Input.GetMouseButtonDown(0))
        {
            selected = true;
        }
    }

    protected void Update()
    {
        // if mouse left click is not being held down, release selection
        if (!Input.GetMouseButton(0))
            selected = false;
    }

    protected void Start()
    {
        selected = false;
    }
    

    protected float tabMovement;
    public float getTabMovement() { return tabMovement; }

}
