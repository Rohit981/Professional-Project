using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour {


    

	// Use this for initialization
	void Start () {
        Debug.Log("RestartScript Running");
    }
	
	// Update is called once per frame
	void Update () {
		


	}
    private void OnTriggerHit (Collider other)
    {
        if (other.tag == "Killer")
        {
            Debug.Log("player hit Restart Point");
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name); ;
        }
    }
}
