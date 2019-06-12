using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Mechanic : MonoBehaviour {

    [SerializeField] [Tooltip("Set this value to be equal to the int value of the layer 'Player'")]
    int playerLayer;

    [SerializeField]
    private float jumpStrenght = 0.5f;

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
        if(other.gameObject.layer == playerLayer)
        {
            other.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpStrenght, ForceMode.Impulse);
        }
    }
}
