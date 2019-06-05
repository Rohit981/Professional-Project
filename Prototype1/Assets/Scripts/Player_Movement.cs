using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

    internal Rigidbody rb;

    private bool movementActivated;

    [Tooltip("X positive, Y and Z zero if you just want the player to be pushed to the right. Press M to make the player move")]
    [SerializeField] Vector3 forceBeingApplied;
    //private float speed;



    // Use this for initialization
    void Start()
    {
        movementActivated = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //float x_value = Input.GetAxis("Horizontal") * -speed;

        if (Input.GetKeyDown("m"))
            movementActivated = true;

        //Vector3 vel = rb.velocity;
        //vel.x = x_value;
        //rb.velocity = vel;

    }

    private void FixedUpdate()
    {
        if (movementActivated)
            rb.AddForce(forceBeingApplied);
    }
}
