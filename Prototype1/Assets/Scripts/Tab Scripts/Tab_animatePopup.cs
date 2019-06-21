using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab_animatePopup : Tab_operatePopup
{
    // ------------------------------------------------------------------------------------------------------ INSPECTOR INTERFACE - YOU CAN SAFELY TWEAK THESE VALUES
    [SerializeField] private List<Animator> popUpAnimators;
    [Tooltip("How fast the animation plays relative to tab movement")]
    [SerializeField] private float animationSpeed = 0.2f;
    // -------------------------------------------------------------------------------------------------------------------------------------- INSPECTOR INTERFACE END

    private Tab_operateTab operateTabScript;
    private float tabMovement;

    private void Start()
    {
        operateTabScript = GetComponent<Tab_operateTab>();
        if (operateTabScript == null)
        {
            Debug.Log("The object this script is attached to needs to have a script that operates the tab!");
        }
    }

    // modify animation's time value based on how much mouse has moved
    private void playAnimations()
    {
        if (popUpAnimators.Count > 0)
        {
            foreach (Animator anim in popUpAnimators)
            {
                anim.Play(0, 0, anim.GetCurrentAnimatorStateInfo(0).normalizedTime + (tabMovement * animationSpeed));
            }
        }
        else
            Debug.Log("You attached a animate pop up script but you did not give me any pop up to animate! :(");
            
    }

    // Update is called once per frame
    private void Update()
    {
        tabMovement = operateTabScript.getTabMovement();
        playAnimations();
    }
}
