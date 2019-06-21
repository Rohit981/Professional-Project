using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Movement : MonoBehaviour
{

    internal Rigidbody2D rb;
    private bool movementActivated;
    //bool hasAlreadyJumped = false; - unity says this is assigned but never used? temporarily commented it out to get rid of it in the console - MB 

    [SerializeField]
    internal float maxSpeed = 3.0f;

    [SerializeField]
    float acceleration = 9.0f;

    //private bool allDetached = false; - unity says this is assigned but never used? temporarily commented it out to get rid of it in the console - MB 
    private int numberofTotalObjects = 0;
    int counter = 0;


    [SerializeField]
    float jumpStrength;

    [SerializeField]
    float RampSpeed;

    [SerializeField]
    float SlowDownSpeed;

    Animator anim;

    [SerializeField]
    private Animator pageAnim;

    // Use this for initialization
    void Start()
    {
        movementActivated = false;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Use this for physics related code
    void FixedUpdate()
    {
        if (movementActivated)
        {
            if (rb.velocity.magnitude <= maxSpeed)
            {
                rb.AddForce(new Vector2(1, 0) * acceleration);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        numberofTotalObjects = allObjects.Length;


        Scene scene = SceneManager.GetActiveScene();
        if (Input.GetKeyDown("m"))
        {
            movementActivated = true;
            anim.SetBool("isWalking", true);

        }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ramp")
        {
            maxSpeed = RampSpeed;
        }

        else if (collision.gameObject.tag == "Slow")
        {
            acceleration = 0;
            maxSpeed = SlowDownSpeed;
            print("Slow");

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "JumpObject")
        {
            rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);

            print("Jumped");
        }

        if(collision.gameObject.tag == "EndGoal")
        {
            print("Reached End");
            pageAnim.enabled = true;
            this.gameObject.SetActive(false) ;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.SetBool("isJumping", false);
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
    

