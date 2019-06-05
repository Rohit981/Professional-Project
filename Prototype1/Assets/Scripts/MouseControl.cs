using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{

    // Mouse Control Variabbles
    [SerializeField] private float mouseSensitivityX = 10;

    private bool selected = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            if (!Input.GetMouseButton(0))
                selected = false;

            float moveLR = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;

            // add left right mouse movement to the X value of the object's transform
            transform.position += new Vector3(-moveLR, 0, 0);
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
