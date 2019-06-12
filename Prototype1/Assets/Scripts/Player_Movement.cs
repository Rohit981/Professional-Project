﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Movement : MonoBehaviour
{

    internal Rigidbody rb;
    private bool movementActivated;
    bool hasAlreadyJumped = false;

    [SerializeField] internal float maxSpeed = 3.0f;
    [SerializeField] float acceleration = 9.0f;
    private bool allDetached = false;
    private int numberofTotalObjects = 0;
    int counter = 0;


    [SerializeField]
    float jumpStrength;

    [SerializeField]
    float RampSpeed;

    [SerializeField]
    float SlowDownSpeed;

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
                rb.AddForce(new Vector3(1, 0, 0) * acceleration);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        numberofTotalObjects = allObjects.Length;


        Scene scene = SceneManager.GetActiveScene();
        if (Input.GetKeyDown("m"))
            movementActivated = true;


        if (Input.GetKeyDown("r"))
        {
            //  DestroyAll();
            foreach (GameObject gameobject  in allObjects)
            {
                do
                {
                    counter++;

                    gameObject.transform.parent = null;
                   print("Detaching");
                } while (counter<=numberofTotalObjects);
                   
            }

            if (counter >= numberofTotalObjects)
            {
                SceneManager.LoadScene(scene.name);

            }
            
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        

        if (collision.gameObject.tag == "Ramp")
        {
            maxSpeed = RampSpeed; 
        }

        else if(collision.gameObject.tag == "Slow")
        {
            acceleration = 0;
            maxSpeed = SlowDownSpeed;  
            print("Slow");

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "JumpObject")
        {
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
            print("Jumped");
        }
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    if(collision.gameObject.tag == "JumpObject" && hasAlreadyJumped == false)
    //    {
    //        hasAlreadyJumped = true;
    //    }
    //}

    /* Attempt to resolve animation skew on level reset

    private void DestroyAll()
    {
        Debug.Log("KillAll");
        GameObject[] GameObjects = (FindObjectsOfType<GameObject>() as GameObject[]);

        for (int i = 0; i < GameObjects.Length; i++)
        {
            Destroy(GameObjects[i]);
        }
    }

    */

}
    

