using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab_animatePopup : MonoBehaviour
{

    // Inspector
    [Header("Animation settings")]
    [SerializeField] private List<Animator> popUpAnimators;
    [Tooltip("How fast the animation plays relative to tab movement")]
    [SerializeField] private float animationSpeed = 0.2f;

    // Will store slideDirection value found in moveTab script
    private TabSlideDirection slideDirection = TabSlideDirection.Horizontal;

    // Stores last position and current position so we can calculate how much tab has moved since last frame
    private Vector3 lastPosition;
    private Vector3 currentPosition;

    private void Start()
    {
        slideDirection = gameObject.GetComponent<Tab_moveTab>().slideDirection;
        currentPosition = transform.position;
        lastPosition = currentPosition;
       
    }

    // modify animation's time value based on how much mouse has moved
    private void playAnimations(float tabMovement)
    {
        foreach (Animator anim in popUpAnimators)
        {
            anim.Play(0, 0, anim.GetCurrentAnimatorStateInfo(0).normalizedTime + (tabMovement * animationSpeed));
        }
    }

    // Update is called once per frame
    private void Update()
    {
        currentPosition = transform.position;
        if (currentPosition != lastPosition)
        {
            switch (slideDirection)
            {
                case TabSlideDirection.Horizontal:
                    playAnimations(currentPosition.x - lastPosition.x);
                    break;
                case TabSlideDirection.Vertical:
                    playAnimations(currentPosition.z - lastPosition.z);
                    break;
            }
        }
        lastPosition = currentPosition;
    }
}
