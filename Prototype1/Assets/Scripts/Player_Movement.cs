using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    internal Rigidbody rb;
    private bool movementActivated;

    [SerializeField] float maxSpeed = 3.0f;
    [SerializeField] float acceleration = 9.0f;


    // Use this for initialization
    void Start()
    {
        movementActivated = false;
        rb = GetComponent<Rigidbody>();
    }

    // Use this for physics related code
    void FixedUpdate()
    {
        if (movementActivated)
        {
            if (rb.velocity.magnitude <= maxSpeed)
                rb.AddForce(new Vector3(-1, 0, 0) * acceleration);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m"))
            movementActivated = true;
    }
}
