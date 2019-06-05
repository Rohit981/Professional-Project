using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLevelEnd : MonoBehaviour
{

    [Tooltip("this is needed so i can easily access the player's animatior component")]
    [SerializeField] private GameObject player;

    // Use this for initialization
    void Start()
    {
        Debug.Log("console works");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EndGoal")
        {
            player.GetComponent<Animator>().SetBool("ReachedEnd", true);
            Debug.Log("player hit end");
        }
    }
}
