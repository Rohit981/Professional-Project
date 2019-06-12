using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : MonoBehaviour
{
    [SerializeField]
    int PlayerLayer;

    [SerializeField]
    float SlowSpeed;

    public Player_Movement Player;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == PlayerLayer)
        {
            //collision.rigidbody.velocity = collision.rigidbody.velocity * SlowSpeed;
           Player.maxSpeed = SlowSpeed;
        }
    }
}
