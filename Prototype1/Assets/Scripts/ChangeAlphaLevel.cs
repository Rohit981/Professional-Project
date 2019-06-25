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
    Material[] materials;

    // Use this for initialization
    void Start ()
    {
        Trigger = FindObjectOfType<AnimationTrigger>();
        children= GetComponentInChildren<MeshRenderer>();
        materials = GetComponentsInChildren<Material>();

    }

    // Update is called once per frame
    void Update ()
    {
      
            print("Entered");
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

            Color newColor;
            foreach(Material mat in materials)
            {
                newColor = children.material.color;
                newColor.a = alphaLevel;
                children.material.color = newColor;

            }
         

       
                
    }
}
