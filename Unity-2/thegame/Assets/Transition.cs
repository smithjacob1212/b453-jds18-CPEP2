using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Transition : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void FadeIn()
    {
        if (animator == null) animator = GetComponent<Animator>();
        animator.SetTrigger("fade in");
    }

    public void FadeOut()
    {
        if (animator == null) animator = GetComponent<Animator>();
        animator.SetTrigger("fade out");
    }
}
