using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Shot : MonoBehaviour
{
    public Vector2 speed;
    public Collider2D col2D;
    public Rigidbody2D rb2D;
    public ColorEnum color = ColorEnum.pink;
    // Start is called before the first frame update
    void Start()
    {
        col2D = GetComponent<Collider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController p = collision.GetComponent<PlayerController>();
        if (p != null)
        {
            p.ManageObstacleColor(color);
            DestroyShot();
        }
    }

    public void SetVelocity(Vector3 velocity)
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = velocity * speed;
    }

    private void DestroyShot()
    {
        Destroy(this.gameObject);
    }
}
