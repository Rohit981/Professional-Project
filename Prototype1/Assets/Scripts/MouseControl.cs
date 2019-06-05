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

    // Use this for initialization
    void Start()
    {
        leftBoundaryX = gameObject.transform.position.x + leftBoundaryOffset;
        rightBoundaryX = gameObject.transform.position.x - rightBoundaryOffset;
    }

    void OnDrawGizmosSelected()
    {

        // Draws a vertical red line on the left boundary
        Vector3 lineStart = gameObject.transform.position;
        lineStart.x = gameObject.transform.position.x + leftBoundaryOffset;
        Vector3 lineTarget = lineStart;
        lineTarget.y = 20;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(lineStart, lineTarget);

        // Draws a vertical blue line on the right boundary
        lineStart = gameObject.transform.position;
        lineStart.x = gameObject.transform.position.x - rightBoundaryOffset;
        lineTarget = lineStart;
        lineTarget.y = 20;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(lineStart, lineTarget);
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
            }
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
