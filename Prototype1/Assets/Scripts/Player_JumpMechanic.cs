using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_JumpMechanic : MonoBehaviour {

    Rigidbody rb;
    bool Is_Moved = false;

    [SerializeField]
    float force;

   

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown("m"))
        {
            print("Player Moved");           
            Is_Moved = true;
        }

        Movement();
        
    }

    void Movement()
    {
        if (Is_Moved == true)
        {
            rb.AddForce(new Vector3(0, 0, -force), ForceMode.Acceleration);

        }
    }

   

}
