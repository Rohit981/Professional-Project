using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateProps : MonoBehaviour
{
    AnimationTrigger pageChange;

    [SerializeField]
    private float RotationValue;

   
	// Use this for initialization
	void Start ()
    {
        pageChange = FindObjectOfType<AnimationTrigger>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        RotateObjects();
	}

    void RotateObjects()
    {
        Transform[] children = GetComponentsInChildren<Transform>();

        foreach(Transform child in children)
        {
            if(pageChange.Is_Rotating == true)
            {
                             
                
                                                                                     
                child.Rotate(Vector2.left *(RotationValue*Time.deltaTime));
                
            }
        }
    }
}
