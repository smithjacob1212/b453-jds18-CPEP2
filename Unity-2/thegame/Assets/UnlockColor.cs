using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using FMODUnity;

[RequireComponent(typeof(Collider2D))]
public class UnlockColor : MonoBehaviour
{
    public ColorEnum colorToUnlock = ColorEnum.pink;
    private Collider2D col2D;
    public ParticleSystem particles;
    private Light2D light;
    public StudioEventEmitter pickup;
    private void Start()
    {
        col2D = GetComponent<Collider2D>();
        col2D = GetComponent<Collider2D>();
        light = GetComponent<Light2D>();
        pickup = GetComponent<StudioEventEmitter>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RuntimeManager.PlayOneShot(pickup.EventReference);
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player == null) return;
        player.UnlockColor(colorToUnlock);
        DestroyUnlockColor();
    }

    private void DestroyUnlockColor()
    {
        Destroy(this.col2D);
        Destroy(this.light);
        Destroy(this.particles);
    }
}
