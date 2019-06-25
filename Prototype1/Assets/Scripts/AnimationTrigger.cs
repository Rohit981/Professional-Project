using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour {

    [SerializeField]
    private GameObject firstChapterPage;

    [SerializeField]
    private GameObject secondChapterPage;

    internal bool Is_Rotating = false;
    internal bool Is_Alpha = false;

    // Use this for initialization
    void Start ()
    {
        firstChapterPage.SetActive(true);
        secondChapterPage.SetActive(false);
	}
	
	

    void ChapterTansition()
    {
        secondChapterPage.SetActive(true);
        firstChapterPage.SetActive(false);
    }

    void changeRotation()
    {
        Is_Rotating = true;
    }

    void EndRotation()
    {
        Is_Rotating = false;
    }

    void AlphaEffectActive()
    {
        Is_Alpha = true;
    }

    void AlphaEffectNotActive()
    {
        Is_Alpha = false;
    }
}
