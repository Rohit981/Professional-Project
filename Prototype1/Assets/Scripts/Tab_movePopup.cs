using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab_movePopup : MonoBehaviour
{

    // Inspector
    [Header("Movement settings")]
    [Tooltip("You need to drag the parent popUp in here so that both the popup and the shadow will move")]
    [SerializeField] private Transform transformThatWillBeMoved;
    [Tooltip("How fast the popup moves relative to tab movement")]
    [SerializeField] private float movementSpeed = 1.0f;

    // Will store slideDirection value found in moveTab script
    TabSlideDirection slideDirection = TabSlideDirection.Horizontal;

    // Stores last position and current position so we can calculate how much tab has moved since last frame
    Vector3 lastPosition;
    Vector3 currentPosition;

    private void Start()
    {
        Tab_moveTab moveTabScript = gameObject.GetComponent<Tab_moveTab>();
        slideDirection = moveTabScript.slideDirection;
        currentPosition = transform.position;
        lastPosition = currentPosition;

    }

    // Update is called once per frame
    private void Update()
    {
        currentPosition = transform.position;
        if (currentPosition != lastPosition)
        {
            transformThatWillBeMoved.Translate(currentPosition - lastPosition);
        }
        lastPosition = currentPosition;
    }
}
