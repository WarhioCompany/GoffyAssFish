using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackFadeScript : MonoBehaviour
{
    public static BlackFadeScript instance { get; private set; }

    public GameObject fadeObj;
    private Animator anim;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        fadeObj.SetActive(false);
        anim = fadeObj.GetComponent<Animator>();
    }

    public void FadeIn()
    {
        fadeObj.SetActive(true);
        anim.Play("fadeIn");
    }
    
    public void FadeOut()
    {
        StartCoroutine(FadeOutInner());
    }
    public IEnumerator FadeOutInner() 
    {
        anim.Play("fadeOut");
        yield return new WaitForSeconds(1);
        fadeObj.SetActive(false);
    }
}
