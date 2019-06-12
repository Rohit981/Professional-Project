using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Mechanic : MonoBehaviour {

    [SerializeField] [Tooltip("Set this value to be equal to the int value of the layer 'Player'")]
    int playerLayer;

  
    bool hasAlreadyJumped = false;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        DestroyCollider();
	}

    private void OnTriggerEnter(Collider other)
    {
       
    }

    private void OnTriggerExit(Collider other)
    {
        hasAlreadyJumped = true;
    }

    void DestroyCollider()
    {
        if(hasAlreadyJumped == true)
        {
            Destroy(this);
        }
    }
}
