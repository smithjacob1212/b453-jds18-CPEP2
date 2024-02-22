using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]

public class RandomTurret : MonoBehaviour
{
    public Transform spawner;
    public Collider2D playerDetection;
    public Animator animator;
    public Transform playerTransform;
    public GameObject pinkShot; // Uses the gameobject pinkshot
    public GameObject cyanShot; // Uses the gameobject cyanshot
    public GameObject orangeShot; // Uses the gameobject orangeshot
    public float shotCooldown = 2f;
    public float t_shotCooldown = 0f;
    public StudioEventEmitter shoot;
    void Start()
    {
        playerDetection = GetComponentInChildren<Collider2D>();
        animator = GetComponentInChildren<Animator>();
        shoot = GetComponentInChildren<StudioEventEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            SpawnerFollowPlayerAndShoot();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController p = collision.GetComponent<PlayerController>();
        if (p != null)
        {
            playerTransform = p.transform;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController p = collision.GetComponent<PlayerController>();
        if (p != null)
        {
            playerTransform = null;
        }
    }

    public virtual void SpawnerFollowPlayerAndShoot()
    {
        Vector3 delta = playerTransform.position - this.transform.position;
        float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
        spawner.rotation = Quaternion.Euler(0, 0, angle);
        Shoot();
    }

    private void Shoot()
    {
        float randomColor = UnityEngine.Random.Range(1, 4); // Randomly chooses a number to pick between the different color shots

        if (randomColor == 1)
        {
            t_shotCooldown += Time.deltaTime;
            if (t_shotCooldown > 1.5f && t_shotCooldown < 1.6f)
            {
                animator.SetTrigger("Shoot");
                t_shotCooldown = 1.6f;
            }
            if (t_shotCooldown >= shotCooldown)
            {
                RuntimeManager.PlayOneShot(shoot.EventReference);
                GameObject s = Instantiate(pinkShot, spawner.position, spawner.rotation, null); // Spawns the pink shot
                s.GetComponent<Shot>().SetVelocity(s.transform.right);
                t_shotCooldown = 0f;
            }
        }

        if (randomColor == 2)
        {
            t_shotCooldown += Time.deltaTime;
            if (t_shotCooldown > 1.5f && t_shotCooldown < 1.6f)
            {
                animator.SetTrigger("Shoot");
                t_shotCooldown = 1.6f;
            }
            if (t_shotCooldown >= shotCooldown)
            {
                RuntimeManager.PlayOneShot(shoot.EventReference);
                GameObject s = Instantiate(cyanShot, spawner.position, spawner.rotation, null); // Spawns the cyan shot
                s.GetComponent<Shot>().SetVelocity(s.transform.right);
                t_shotCooldown = 0f;
            }
        }

        if (randomColor == 3)
        {
            t_shotCooldown += Time.deltaTime;
            if (t_shotCooldown > 1.5f && t_shotCooldown < 1.6f)
            {
                animator.SetTrigger("Shoot");
                t_shotCooldown = 1.6f;
            }
            if (t_shotCooldown >= shotCooldown)
            {
                RuntimeManager.PlayOneShot(shoot.EventReference);
                GameObject s = Instantiate(orangeShot, spawner.position, spawner.rotation, null); // Spawns the orange shot
                s.GetComponent<Shot>().SetVelocity(s.transform.right);
                t_shotCooldown = 0f;
            }
        }
    }
}
