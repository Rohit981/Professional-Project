using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeChildofTabAfterAnimation : MonoBehaviour {

    [SerializeField] private GameObject tabThatWillBeParent;

    private bool hasParented = false;

	// Use this for initialization
	void Start ()
    {
        tabThatWillBeParent.transform.DetachChildren();
    }
	
	// Update is called once per frame
	void Update () {
		if (!hasParented)
        {
            if (Time.time > 1.2f)
            {
                gameObject.transform.parent = tabThatWillBeParent.transform;
                hasParented = true;
            }
        }
	}
}
