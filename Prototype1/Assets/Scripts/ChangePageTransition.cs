using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePageTransition : MonoBehaviour {

    [SerializeField]
    private GameObject firstChapterPage;

    [SerializeField]
    private GameObject secondChapterPage;

   
    // Use this for initialization
    void Start ()
    {
        firstChapterPage.SetActive(true);
        secondChapterPage.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void ChapterTansition()
    {
        secondChapterPage.SetActive(true);
        firstChapterPage.SetActive(false);
    }

   
}
