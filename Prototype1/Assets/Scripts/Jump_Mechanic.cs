using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Mechanic : MonoBehaviour {

    [SerializeField]
    int PlayerLayer;

    [SerializeField]
    int height;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == PlayerLayer)
        {
            other.GetComponent<Rigidbody>().velocity = Vector3.up * height;
        }
    }
}
