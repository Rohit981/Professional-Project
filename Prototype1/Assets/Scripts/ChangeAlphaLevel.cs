using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAlphaLevel : MonoBehaviour
{
    [SerializeField]
   private float alphaLevel;

   private float time;
   private bool is_Timing = true;

    //Variable that changes the anim time and when it reaches that time then it reaches Zero value
    [SerializeField]
    private float changeAnimtime;

    [SerializeField]
    private float AlphaLevelChange;

   private float childrenSize;

    AnimationTrigger Trigger;
    MeshRenderer children;
    
    Material[] mats;

    Renderer[] rends;
   

    // Use this for initialization
    void Start ()
    {
        Trigger = FindObjectOfType<AnimationTrigger>();
        children= GetComponentInChildren<MeshRenderer>();
        mats = GetComponentInChildren<Renderer>().materials;
        rends = GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update ()
    {
      
            print("Entered");
            //if(is_Timing == true)
            //{
            //    time += Time.deltaTime;

            //}
            //if(alphaLevel <= 0.2f)
            //{
            //    time = 0f;
            //    is_Timing = false;
            //}

            //if (time >= changeAnimtime)
            //{
            //    alphaLevel -= AlphaLevelChange;
            //    time = 0f;
            //}

            Color newColor;

        foreach (Renderer rend in rends)
        {
            
            for(int i = 0; i < rend.materials.Length; i++)
            {
                
                newColor = rend.material.color ;
                newColor.a -= alphaLevel * Time.deltaTime;
                rend.material.color = newColor;
                print("Change Alpha level");

            }

        }







    }
}
