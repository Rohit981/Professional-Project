using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateCamera : MonoBehaviour
{
    Camera cam;

    [SerializeField]
    private float rotationchangeX;

    [SerializeField]
    private float rotationchangeY;

    [SerializeField]
    private float rotationchangeZ;

    [SerializeField]
    public Transform Target;

    // Use this for initialization
    void Start ()
    {
        cam = FindObjectOfType<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
    {


    }

    public void RotateToRight()
    {
        cam.transform.LookAt(Target);
        Target.Rotate(new Vector3(Target.rotation.x + rotationchangeX, Target.rotation.y - rotationchangeY, Target.rotation.z - rotationchangeZ), Space.World);

    }

    public void RotateToLeft()
    {
        cam.transform.LookAt(Target);
        Target.Rotate(new Vector3(Target.rotation.x + rotationchangeX, Target.rotation.y - rotationchangeY, Target.rotation.z - rotationchangeZ), Space.World);
    }
}
