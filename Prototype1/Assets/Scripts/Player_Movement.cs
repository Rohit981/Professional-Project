using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

    internal Rigidbody rb;

    [SerializeField]
    private float speed;



    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x_value = Input.GetAxis("Horizontal") * -speed;

        Vector3 vel = rb.velocity;

        vel.x = x_value;
        rb.velocity = vel;

    }
}
