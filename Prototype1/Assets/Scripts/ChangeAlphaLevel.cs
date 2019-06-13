using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAlphaLevel : MonoBehaviour
{
    [SerializeField]
    float alphaLevel;

    float time;
    bool is_Timing = true;

    //Variable that changes the anim time and when it reaches that time then it reaches Zero value
    [SerializeField]
    float changeAnimtime;

    [SerializeField]
    float AlphaLevelChange;

    float childrenSize;

    
     

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(is_Timing == true)
        {
            time += Time.deltaTime;

        }
        if(alphaLevel <= 0.2f)
        {
            time = 0f;
            is_Timing = false;
        }

        if (time >= changeAnimtime)
        {
            alphaLevel -= AlphaLevelChange;
            time = 0f;
        }

        MeshRenderer[] children =  GetComponentsInChildren<MeshRenderer>();

        Color newColor;

        foreach(MeshRenderer child in children)
        {
            newColor = child.material.color;
            newColor.a = alphaLevel;
            child.material.color = newColor;
        }
        
        
    

    }
}
