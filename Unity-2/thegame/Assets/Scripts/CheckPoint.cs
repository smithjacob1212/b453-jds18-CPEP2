using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FMODUnity;
[RequireComponent(typeof(Animator))]
public class CheckPoint : MonoBehaviour
{
    public bool activated = false;
    private UnityEngine.Rendering.Universal.Light2D light;
    private Animator animator;
    public StudioEventEmitter checkpointSound;

    private void Start()
    {
        light = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        animator = GetComponent<Animator>();
        checkpointSound = GetComponent<StudioEventEmitter>();
        UnActivateCheckPoint();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            ActivateCheckPoint();
        }
    }

    public void ActivateCheckPoint()
    {
        RuntimeManager.PlayOneShot(checkpointSound.EventReference);
        activated = true;
        animator.SetBool("isActive", true);
        light.enabled = true;
    }

    public void UnActivateCheckPoint()
    {
        activated = false;
        animator.SetBool("isActive", false);
        light.enabled = false;
    }
}
