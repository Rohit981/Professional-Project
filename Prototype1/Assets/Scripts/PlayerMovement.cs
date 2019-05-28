using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Rigidbody rb;


    [SerializeField]
    private float speed;

  
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float z_value = Input.GetAxis("Horizontal") * speed;
      

        Vector3 vel = rb.velocity;


        vel.z = z_value;
        rb.velocity = vel;

    }
}

