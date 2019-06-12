using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampSpeedUp : MonoBehaviour {
    [SerializeField]
    [Tooltip("Set this value to be equal to the int value of the layer 'Player'")]
    int playerLayer;

    [SerializeField]
    int Acceleration;

    [SerializeField]
    public Player_Movement player;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == playerLayer )
        {
            player.maxSpeed = Acceleration;
        }
    }

  
}
