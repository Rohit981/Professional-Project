using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab_animatePopup : Tab_operatePopup
{
    // ------------------------------------------------------------------------------------------------------ INSPECTOR INTERFACE - YOU CAN SAFELY TWEAK THESE VALUES
    [Header("")]
    [SerializeField] private List<Animator> animateMe;

    [Header("")]
    [Tooltip("If this is set to false, the animation will go from 0% to 100% based on how far the tab is from its limits. If set to true, the animation will just play at the desidred speed and will loop when it reaches the end.")]
    [SerializeField] private bool allowLooping = false;

    [ConditionalHide("allowLooping", true)]
    [Tooltip("How fast the animation plays relative to tab movement")]
    [SerializeField] private float animationSpeed = 0.2f;

    [Header("Read tooltip!")]
    [Tooltip("This is a temporary fix. Because most animations we built so far already have a reversed animation built-in, this variable will make them work when using the new percentage feature. Hopefully animations will not have the reversed animation built-in in our final versions, so I will be able to remove this stupid boolean.")]
    [SerializeField] private bool animationHasBuiltInReverse = true;
    // -------------------------------------------------------------------------------------------------------------------------------------- INSPECTOR INTERFACE END

    private Tab_operateTab operateTabScript;
    private float tabMovement;
    private float tabMovementPercentage;

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
        if (animateMe.Count > 0)
        {
            foreach (Animator anim in animateMe)
            {
                if (allowLooping)
                {
                    anim.Play(0, 0, anim.GetCurrentAnimatorStateInfo(0).normalizedTime + (tabMovement * animationSpeed));
                }
                else
                {
                    if (animationHasBuiltInReverse)
                        anim.Play(0, 0, (anim.GetCurrentAnimatorClipInfo(0).Length) * (tabMovementPercentage / 2));
                    else
                        anim.Play(0, 0, anim.GetCurrentAnimatorClipInfo(0).Length * tabMovementPercentage);
                }
            }
        }
        else
            Debug.Log("You attached an animate pop up script but you did not give me any pop up to animate! :(");

    }

    // Update is called once per frame
    private void Update()
    {
        tabMovement = operateTabScript.tabMovement;
        tabMovementPercentage = operateTabScript.TabMovementPercentage;
        playAnimations();
    }
}