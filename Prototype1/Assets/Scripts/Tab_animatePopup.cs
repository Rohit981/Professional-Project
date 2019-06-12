using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab_animatePopup : MonoBehaviour {

    public enum TabSlideDirection { Horizontal, Vertical };

    [Header("Tab settings")]
    [SerializeField] private TabSlideDirection slideDirection;
    [SerializeField] private float mouseSensitivity = 10;

    [Header("Animation")]
    [SerializeField] private Animator popUpAnimator;
    [SerializeField] private string animationName;
    [SerializeField] private float animationSpeed = 0.2f;

    private bool selected = false;

    // This runs when the mouse is hovering over the Tab
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selected = true;
        }
    }

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update () {
        if (selected)
        {
            if (!Input.GetMouseButton(0))
                selected = false;

            float movementMagnitude = 1.0f;

            switch (slideDirection)
            {
                case TabSlideDirection.Horizontal:
                    movementMagnitude = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
                    // add left right mouse movement to the X value of the object's transform
                    transform.position += new Vector3(movementMagnitude, 0, 0);
                    break;

                case TabSlideDirection.Vertical:
                    movementMagnitude = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
                    // add left right mouse movement to the Z value of the object's transform
                    transform.position += new Vector3(0, 0, movementMagnitude);
                    break;
            }


            popUpAnimator.Play(animationName, 0, popUpAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime + (movementMagnitude * animationSpeed));
            

        }
    }
}
