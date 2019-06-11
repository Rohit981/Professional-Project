using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{

    // ------------------------------- ALL THE X VALUES ARE INVERTED BECAUSE THAT'S THE WAY THE SCENE WAS BUILT! GOING TO THE RIGHT DECREASES THE X!

    // Mouse Control Variabbles
    [SerializeField] private float mouseSensitivityX = 10;
    [SerializeField] private float leftBoundaryOffset = 4;
    [SerializeField] private float rightBoundaryOffset = 2;

    private float leftBoundaryX;
    private float rightBoundaryX;

    private bool selected = false;
    private bool soundIsPlaying = false;


    // Use this for initialization
    void Start()
    {
        GetComponent<AudioSource>().enabled = true;
        leftBoundaryX = gameObject.transform.position.x + leftBoundaryOffset;
        rightBoundaryX = gameObject.transform.position.x - rightBoundaryOffset;


        AimLeftGizmo();
        AimRightGizmo();
    }


    private Vector3 leftLineStart;
    private Vector3 leftLineTarget;
    private Vector3 rightLineStart;
    private Vector3 rightLineTarget;

    private void AimLeftGizmo()
    {
        leftLineStart = gameObject.transform.position;
        leftLineStart.x = gameObject.transform.position.x + leftBoundaryOffset;
        leftLineTarget = leftLineStart;
        leftLineTarget.y = 20;
    }

    private void AimRightGizmo()
    {
        rightLineStart = gameObject.transform.position;
        rightLineStart.x = gameObject.transform.position.x - rightBoundaryOffset;
        rightLineTarget = leftLineStart;
        rightLineTarget.y = 20;
    }


    void OnDrawGizmosSelected()
    {
        // Draws a vertical red line on the left boundary
        if (!Application.isPlaying)
        {
            AimLeftGizmo();
        }
        Gizmos.color = Color.red;
        Gizmos.DrawLine(leftLineStart, leftLineTarget);

        // Draws a vertical blue line on the right boundary
        if (!Application.isPlaying)
        {
            AimRightGizmo();
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(rightLineStart, rightLineTarget);
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            if (!Input.GetMouseButton(0))
                selected = false;


            float moveLR = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;

            if ((moveLR < 0 && gameObject.transform.position.x < leftBoundaryX) || (moveLR > 0 && gameObject.transform.position.x > rightBoundaryX))
            {
                // add left right mouse movement to the X value of the object's transform
                transform.position += new Vector3(-moveLR, 0, 0);
                if (!soundIsPlaying)
                {
                    GetComponent<AudioSource>().Play();
                    soundIsPlaying = true;
                }
            }
        }
        else
        {
            GetComponent<AudioSource>().Stop();
            soundIsPlaying = false;
        }
    }

    private void OnMouseDown()
    {

    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selected = true;
        }
    }
}
